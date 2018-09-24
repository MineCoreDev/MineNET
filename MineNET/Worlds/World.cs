using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MineNET.BlockEntities;
using MineNET.Blocks;
using MineNET.Data;
using MineNET.Entities;
using MineNET.Entities.Players;
using MineNET.Events.PlayerEvents;
using MineNET.IO;
using MineNET.Items;
using MineNET.Network.MinecraftPackets;
using MineNET.Values;
using MineNET.Worlds.Dimensions;
using MineNET.Worlds.Formats.WorldSaveFormats;
using MineNET.Worlds.Generators;

namespace MineNET.Worlds
{
    public sealed class World
    {
        public const int MAX_HEIGHT = 256;

        public const int BLOCK_UPDATE_NORMAL = 1;
        public const int BLOCK_UPDATE_RANDOM = 2;
        public const int BLOCK_UPDATE_SCHEDULED = 3;
        public const int BLOCK_UPDATE_WEAK = 4;
        public const int BLOCK_UPDATE_TOUCH = 5;
        public const int BLOCK_UPDATE_REDSTONE = 6;
        public const int BLOCK_UPDATE_TICK = 7;

        #region Static Method

        public static void CreateWorld(string worldName)
        {
            World.CreateWorld(worldName, new AnvilWorldSaveFormat(worldName));
        }

        public static void CreateWorld(string worldName, IWorldSaveFormat format)
        {
            World world = new World(worldName, format);
            world.Format.WorldData.Create(world);

            Server.Instance.Worlds.Add(worldName, world);
        }

        public static void LoadWorld(string worldName)
        {
            World.LoadWorld(worldName, new AnvilWorldSaveFormat(worldName));
        }

        public static void LoadWorld(string worldName, IWorldSaveFormat format)
        {
            if (World.Exists(worldName))
            {
                World world = new World(worldName, format);
                world.Format.WorldData.Load(world);

                Server.Instance.Worlds.Add(worldName, world);
            }
            else
            {
                CreateWorld(worldName);
            }
        }

        public static World GetWorld(string worldName)
        {
            if (World.Exists(worldName))
            {
                if (World.HasLoadedWorld(worldName))
                {
                    return Server.Instance.Worlds[worldName];
                }
            }

            return null;
        }

        public static World GetMainWorld()
        {
            return Server.Instance.Worlds[Server.Instance.ServerProperty.MainWorldName];
        }

        public static bool HasLoadedWorld(string worldName)
        {
            if (Server.Instance.Worlds.ContainsKey(worldName))
            {
                return true;
            }

            return false;
        }

        public static bool Exists(string worldName)
        {
            if (Directory.Exists($"{Server.ExecutePath}\\worlds\\{worldName}"))
            {
                if (File.Exists($"{Server.ExecutePath}\\worlds\\{worldName}\\level.dat"))
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        public string Name { get; set; } = "World";
        public int Seed { get; private set; } = -1;
        public byte Dimension { get; private set; } = DimensionIDs.OverWorld;
        public int GeneratorType { get; private set; } = GeneratorIDs.Infinite;
        public GameMode Gamemode { get; set; } = GameMode.Survival;
        public Difficulty Difficulty { get; set; } = Difficulty.Normal;

        public IWorldSaveFormat Format { get; }
        public IGenerator Generator { get; }

        public int SpawnX { get; set; } = 128;
        public int SpawnY { get; set; } = 5;
        public int SpawnZ { get; set; } = 128;

        public long LastPlayed { get; internal set; }

        private Dictionary<Tuple<int, int>, Chunk> Chunks { get; } = new Dictionary<Tuple<int, int>, Chunk>();

        private Dictionary<long, Player> Players { get; } = new Dictionary<long, Player>();
        private Dictionary<long, Entity> Entities { get; } = new Dictionary<long, Entity>();
        private List<BlockEntity> BlockEntities { get; set; } = new List<BlockEntity>();

        private ConcurrentDictionary<Block, int> updateQueue = new ConcurrentDictionary<Block, int>();

        private World(string worldName, IWorldSaveFormat format)
        {
            this.Name = worldName;
            this.Format = format;

            string generatorName = Server.Instance.ServerProperty.WorldGenerator.ToUpper();
            this.Generator = GeneratorManager.GetGenerator(generatorName);
            if (this.Generator == null)
            {
                this.Generator = GeneratorManager.GetGenerator("FLAT");
            }
        }

        public void UpdateTick(long tick)
        {
            Entity[] entities = this.Entities.Values.ToArray();
            for (int i = 0; i < entities.Length; ++i)
            {
                entities[i].UpdateTick(tick);
            }

            BlockEntity[] blockEntities = this.BlockEntities.ToArray();
            for (int i = 0; i < blockEntities.Length; ++i)
            {
                //blockEntities[i].OnUpdate(tick);
            }

            foreach (KeyValuePair<Block, int> pair in this.updateQueue)
            {
                int time = pair.Value - 1;
                Block block = pair.Key;
                if (time < 1)
                {
                    block.UpdateTick(World.BLOCK_UPDATE_SCHEDULED);
                    this.updateQueue.TryRemove(block, out time);
                    continue;
                }

                this.updateQueue[block] = time;
            }
        }

        public Block GetBlock(Vector3 pos)
        {
            Tuple<int, int> chunkPos = new Tuple<int, int>((int) pos.X >> 4, (int) pos.Z >> 4);
            Chunk chunk = this.GetChunk(chunkPos);

            int id = chunk.GetBlock(pos.FloorX & 0x0f, (int) pos.Y & 0xff, pos.FloorZ & 0x0f);
            byte meta = chunk.GetMetadata(pos.FloorX & 0x0f, (int) pos.Y & 0xff, pos.FloorZ & 0x0f);

            Block block = Block.Get(id, meta);
            block.Position = pos.Position(this);

            return block;
        }

        public void SetBlock(Vector3 pos, Block block)
        {
            this.SetBlock(pos, block, false);
        }

        public void SetBlock(Vector3 pos, Block block, bool flagAll)
        {
            Tuple<int, int> chunkPos = new Tuple<int, int>(pos.FloorX >> 4, pos.FloorZ >> 4);
            Chunk chunk = this.GetChunk(chunkPos);

            chunk.SetBlock(pos.FloorX & 0x0f, pos.FloorY & 0xff, pos.FloorZ & 0x0f, block.ID);
            chunk.SetMetadata(pos.FloorX & 0x0f, pos.FloorY & 0xff, pos.FloorZ & 0x0f, (byte) block.Damage);

            if (flagAll)
            {
                this.SendBlocks(Server.Instance.GetPlayers(), new Vector3[] { pos }, UpdateBlockPacket.FLAG_ALL_PRIORITY);
            }
            else
            {
                this.SendBlocks(Server.Instance.GetPlayers(), new Vector3[] { pos });
            }
        }

        public Chunk GetChunk(Tuple<int, int> chunkPos)
        {
            Chunk chunk = null;
            if (this.Chunks.ContainsKey(chunkPos))
            {
                chunk = this.Chunks[chunkPos];
            }
            else
            {
                chunk = this.Format.GetChunk(chunkPos.Item1, chunkPos.Item2);
            }

            chunk.InternalSetWorld(this);

            return chunk;
        }

        public IEnumerable<Chunk> LoadChunks(Player player, int radius)
        {
            Dictionary<Tuple<int, int>, double> newOrders = new Dictionary<Tuple<int, int>, double>();

            double radiusSquared = Math.Pow(radius, 2);
            Vector2 center = player.GetChunkVector();

            lock (this.Chunks)
            {
                for (int x = -radius; x <= radius; ++x)
                {
                    for (int z = -radius; z <= radius; ++z)
                    {
                        int distance = (x * x) + (z * z);
                        if (distance > radiusSquared)
                        {
                            continue;
                        }

                        int chunkX = (int) (x + center.X);
                        int chunkZ = (int) (z + center.Y);
                        Tuple<int, int> index = new Tuple<int, int>(chunkX, chunkZ);
                        newOrders[index] = distance;
                    }
                }

                foreach (Tuple<int, int> chunkKey in this.Chunks.Keys.ToArray())
                {
                    if (!newOrders.ContainsKey(chunkKey))
                    {
                        if (!this.HasChunkLoadedByPlayer(chunkKey, player))
                        {
                            this.Format.SetChunk(this.Chunks[chunkKey]);
                            this.Chunks.Remove(chunkKey);
                        }

                        double value;
                        player.LoadedChunks.TryRemove(chunkKey, out value);
                    }
                }

                foreach (var pair in newOrders.OrderBy(pair => pair.Value))
                {
                    if (player.LoadedChunks.ContainsKey(pair.Key)) continue;

                    Chunk chunk = null;
                    try
                    {
                        chunk = this.GetChunk(pair.Key);

                        if (!this.Chunks.ContainsKey(pair.Key))
                        {
                            this.Chunks.Add(pair.Key, chunk);
                        }

                        player.LoadedChunks.TryAdd(pair.Key, pair.Value);
                        this.Generator.ChunkGeneration(chunk);
                    }
                    catch (Exception e)
                    {
                        Logger.Error(e);
                    }

                    yield return chunk;
                }
            }
        }

        public void UnLoadChunks(Player player)
        {
            lock (this.Chunks)
            {
                foreach (Tuple<int, int> chunkKey in this.Chunks.Keys.ToArray())
                {
                    if (!player.LoadedChunks.ContainsKey(chunkKey))
                    {
                        if (!this.HasChunkLoadedByPlayer(chunkKey, player))
                        {
                            this.Format.SetChunk(this.Chunks[chunkKey]);
                            this.Chunks.Remove(chunkKey);
                        }
                    }
                }
            }
        }

        public bool HasChunkLoadedByPlayer(Tuple<int, int> chunkPos, Player notCheckPlayer = null)
        {
            Player[] players = Server.Instance.GetPlayers();
            for (int i = 0; i < players.Length; ++i)
            {
                if (notCheckPlayer != null)
                {
                    if (notCheckPlayer.Name == players[i].Name)
                    {
                        continue;
                    }
                }

                bool r = players[i].LoadedChunks.ContainsKey(chunkPos);
                if (r)
                {
                    return true;
                }
            }

            return false;
        }

        public void Save()
        {
            this.Format.Save(this.Chunks);
        }

        public void SendBlocks(Player[] players, Vector3[] vector3, uint flags = UpdateBlockPacket.FLAG_NONE)
        {
            for (int i = 0; i < vector3.Length; ++i)
            {
                Block block = this.GetBlock(vector3[i]);
                UpdateBlockPacket pk = new UpdateBlockPacket();
                pk.Position = (BlockCoordinate3D) vector3[i];
                pk.RuntimeId = (uint) block.RuntimeId;
                pk.Flags = flags;
                for (int j = 0; j < players.Length; ++j)
                {
                    players[j].SendPacket(pk);
                }
            }
        }

        public void UseItem(Vector3 pos, ItemStack item, BlockFace blockFace, Vector3 clickPos, Player player)
        {
            Block clicked = this.GetBlock(pos);
            Block replace = clicked.GetSideBlock(blockFace);

            if (clicked.Position.Y > 255 || clicked.Position.Y < 0 || clicked.ID == BlockIDs.AIR)
            {
                return;
            }

            PlayerInteractEventArgs playerInteractEvent = new PlayerInteractEventArgs(player, item, clicked, blockFace);

            if (player.IsAdventure)
            {
                playerInteractEvent.IsCancel = true;
            }

            /*if (Server.ServerConfig.SpawnProtection > 0 || player.Op)
            {
                //TODO
            }*/

            /*PlayerEvents.OnPlayerInteract(playerInteractEvent);
            if (playerInteractEvent.IsCancel)
            {
                return;
            }*/

            clicked.UpdateTick(World.BLOCK_UPDATE_TOUCH);
            if (!player.Sneaking && clicked.CanBeActivated && clicked.Activate(player, item))
            {
                return;
            }

            if (!player.Sneaking && item.Item.CanBeActivate &&
                item.Item.Activate(player, this, clicked, blockFace, clickPos))
            {
                if (item.Count <= 0)
                {
                    return;
                }
            }

            if (!item.Item.CanBePlace)
            {
                return;
            }

            Block hand = item.Item.Block;
            hand.Position = replace.Position;

            if (clicked.CanBeReplaced)
            {
                replace = clicked;
                hand.Position = replace.Position;
            }

            //TODO : near by entity check

            //TODO : check can place on

            //BlockPlaceEventArgs blockPlaceEvent = new BlockPlaceEventArgs(player, hand, replace, clicked, item);

            //TODO : check spawn protection

            /*BlockEvents.OnBlockPlace(blockPlaceEvent);
            if (blockPlaceEvent.IsCancel)
            {
                return;
            }*/
            hand.Place(clicked, replace, blockFace, clickPos, player, item);

            LevelSoundEventPacket pk = new LevelSoundEventPacket();
            pk.Position = (Vector3) hand.Position;
            pk.Sound = LevelSoundEventPacket.SOUND_PLACE;
            pk.ExtraData = hand.RuntimeId;
            pk.Pitch = 1;
            player.SendPacket(pk); //TODO : near players
        }

        public void UseBreak(Vector3 pos, ItemStack item, Player player)
        {
            if (player.IsSpectator)
            {
                return;
            }

            Block block = this.GetBlock(pos);

            //BlockBreakEventArgs blockBreakEvent = new BlockBreakEventArgs(player, block, item);

            /*if (player.IsSurvival && !block.CanBreak)
            {
                blockBreakEvent.IsCancel = true;
            }*/

            /*if (Server.ServerConfig.SpawnProtection > 0 || player.Op)
            {
                //TODO
            }*/

            /*BlockEvents.OnBlockBreak(blockBreakEvent);
            if (blockBreakEvent.IsCancel)
            {
                return;
            }*/

            //ItemStack[] drops = blockBreakEvent.Drops;
            ItemStack[] drops = block.GetDrops(item);

            //TODO : can destroy

            LevelEventPacket pk = new LevelEventPacket();
            pk.EventId = LevelEventPacket.EVENT_PARTICLE_DESTROY;
            pk.Position = pos + new Vector3(0.5f, 0.5f, 0.5f);
            pk.Data = block.RuntimeId;
            player.SendPacket(pk); //TODO : near players

            block.Break(player, item);

            item.Item.BlockDestroyed(block, player);

            //TODO : item drop
        }

        public void AddEntity(Entity entity)
        {
            if (entity.World.Name != this.Name)
            {
                return;
            }
            this.Entities[entity.EntityID] = entity;
            entity.SpawnToAll();
        }

        public void RemoveEntity(Entity entity)
        {
            if (!this.Entities.ContainsKey(entity.EntityID))
            {
                return;
            }
            this.Entities.Remove(entity.EntityID);
            entity.DespawnFromAll();
        }

        internal void AddPlayer(Player player)
        {
            if (player.World.Name != this.Name)
            {
                return;
            }

            Player[] players = this.Players.Values.ToArray();
            for (int i = 0; i < players.Length; ++i)
            {
                if (players[i].Uuid.ToString() != player.Uuid.ToString())
                {
                    players[i].SpawnTo(player);
                }
            }

            this.Players[player.EntityID] = player;
            player.SpawnToAll();
        }

        internal void RemovePlayer(Player player)
        {
            if (!this.Players.ContainsKey(player.EntityID))
            {
                return;
            }
            this.Players.Remove(player.EntityID);
            player.DespawnFromAll();
        }

        public Entity GetEntity(long entityID)
        {
            if (this.Entities.ContainsKey(entityID))
            {
                return this.Entities[entityID];
            }
            return null;
        }

        public void AddBlockEntity(BlockEntity blockEntity)
        {
            if (blockEntity.World.Name != this.Name)
            {
                return;
            }
            this.BlockEntities.Add(blockEntity);
        }

        public void RemoveBlockEntity(BlockEntity blockEntity)
        {
            if (!this.BlockEntities.Contains(blockEntity))
            {
                return;
            }

            this.BlockEntities.Remove(blockEntity);
        }

        public void ScheduleUpdate(Block block, int tick)
        {
            this.updateQueue.TryAdd(block, tick);
        }

        public Vector3 GetWorldSpawn()
        {
            return new Vector3(this.SpawnX, this.SpawnY, this.SpawnZ);
        }

        public void SetWorldSpawn(Vector3 spawn)
        {
            this.SpawnX = spawn.FloorX;
            this.SpawnY = spawn.FloorY;
            this.SpawnZ = spawn.FloorZ;
        }
    }
}