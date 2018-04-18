using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MineNET.BlockEntities;
using MineNET.Blocks;
using MineNET.Blocks.Data;
using MineNET.Entities;
using MineNET.Entities.Data;
using MineNET.Entities.Players;
using MineNET.Events.BlockEvents;
using MineNET.Events.PlayerEvents;
using MineNET.Items;
using MineNET.Network.Packets;
using MineNET.Utils;
using MineNET.Values;
using MineNET.Worlds.Formats.WorldSaveFormats;
using MineNET.Worlds.Generator;

namespace MineNET.Worlds
{
    public class World
    {
        public const int MAX_HEIGHT = 256;

        public const int BLOCK_UPDATE_NORMAL = 1;
        public const int BLOCK_UPDATE_RANDOM = 2;
        public const int BLOCK_UPDATE_SCHEDULED = 3;
        public const int BLOCK_UPDATE_WEAK = 4;
        public const int BLOCK_UPDATE_TOUCH = 5;
        public const int BLOCK_UPDATE_REDSTONE = 6;
        public const int BLOCK_UPDATE_TICK = 7;

        public static void CreateWorld(string worldName)
        {
            World.CreateWorld(worldName, new RegionWorldSaveFormat(worldName));
        }

        public static void CreateWorld(string worldName, IWorldSaveFormat format)
        {
            World world = new World();
            world.Format = format;

            world.Name = worldName;
            world.Format.WorldData.Create(world);

            Server.Instance.worlds.Add(worldName, world);
        }

        public static void LoadWorld(string worldName)
        {
            World.LoadWorld(worldName, new RegionWorldSaveFormat(worldName));
        }

        public static void LoadWorld(string worldName, IWorldSaveFormat format)
        {
            if (World.Exists(worldName))
            {
                World world = new World();
                world.Format = format;
                world.Name = worldName;
                world.Format.WorldData.Load(world);

                Server.Instance.worlds.Add(worldName, world);
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
                    return Server.Instance.worlds[worldName];
                }
            }

            return null;
        }

        public static World GetMainWorld()
        {
            return Server.Instance.worlds[Server.ServerConfig.MainWorldName];
        }

        public static bool HasLoadedWorld(string worldName)
        {
            if (Server.Instance.worlds.ContainsKey(worldName))
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

        public IWorldSaveFormat Format { get; set; }
        public IGenerator Generator { get; private set; }
        public string Name { get; private set; }
        public string GeneratorName { get; private set; }

        public long Seed { get; private set; } = 0L;
        public long LastPlayed { get; private set; }

        public Vector3 SpawnPoint { get; set; } = new Vector3(128f, 6f, 128f);

        public GameMode DefaultGameMode { get; set; } = GameMode.Survival;
        public int Difficulty { get; set; } = 1;

        public Dictionary<Tuple<int, int>, Chunk> chunks = new Dictionary<Tuple<int, int>, Chunk>();

        public Dictionary<long, Player> Players { get; set; } = new Dictionary<long, Player>();

        public Dictionary<long, Entity> Entities { get; set; } = new Dictionary<long, Entity>();

        private List<BlockEntity> BlockEntities { get; set; } = new List<BlockEntity>();

        public World()
        {
            string generatorName = Server.ServerConfig.WorldGenerator.ToUpper();
            this.Generator = GeneratorManager.GetGenerator(generatorName);
            if (this.Generator == null)
            {
                this.Generator = GeneratorManager.GetGenerator("FLAT");
            }
        }

        public void OnUpdate(int tick)
        {
            Entity[] entities = this.Entities.Values.ToArray();
            for (int i = 0; i < entities.Length; ++i)
            {
                entities[i].OnUpdate(tick);
            }

            BlockEntity[] blockEntities = this.BlockEntities.ToArray();
            for (int i = 0; i < blockEntities.Length; ++i)
            {
                //blockEntities[i].OnUpdate(tick);
            }
        }

        public Block GetBlock(Vector3 pos)
        {
            Tuple<int, int> chunkPos = new Tuple<int, int>((int) pos.X >> 4, (int) pos.Z >> 4);
            Chunk chunk = this.GetChunk(chunkPos);

            byte id = chunk.GetBlock(pos.FloorX & 0x0f, (int) pos.Y & 0xff, pos.FloorZ & 0x0f);
            byte meta = chunk.GetMetadata(pos.FloorX & 0x0f, (int) pos.Y & 0xff, pos.FloorZ & 0x0f);

            Block block = Block.Get(id, meta);
            block.Position = pos.Position(this);

            return block;
        }

        public void SetBlock(Vector3 pos, Block block)
        {
            Tuple<int, int> chunkPos = new Tuple<int, int>(pos.FloorX >> 4, pos.FloorZ >> 4);
            Chunk chunk = this.GetChunk(chunkPos);

            chunk.SetBlock(pos.FloorX & 0x0f, pos.FloorY & 0xff, pos.FloorZ & 0x0f, (byte) block.ID);
            chunk.SetMetadata(pos.FloorX & 0x0f, pos.FloorY & 0xff, pos.FloorZ & 0x0f, (byte) block.Damage);

            this.SendBlocks(Server.Instance.GetPlayers(), new Vector3[] { pos });
        }

        public Chunk GetChunk(Tuple<int, int> chunkPos)
        {
            Chunk chunk = null;
            if (this.chunks.ContainsKey(chunkPos))
            {
                chunk = this.chunks[chunkPos];
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
            lock (this.chunks)
            {
                Dictionary<Tuple<int, int>, double> newOrders = new Dictionary<Tuple<int, int>, double>();

                double radiusSquared = Math.Pow(radius, 2);
                Vector2 center = player.GetChunkVector();

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

                foreach (Tuple<int, int> chunkKey in this.chunks.Keys.ToArray())
                {
                    if (!newOrders.ContainsKey(chunkKey))
                    {
                        if (!this.HasChunkLoadedByPlayer(chunkKey, player))
                        {
                            //this.Format.SetChunk(chunks[chunkKey]);//TODO:
                            this.chunks.Remove(chunkKey);
                        }
                        player.loadedChunk.Remove(chunkKey);
                    }
                }

                foreach (var pair in newOrders.OrderBy(pair => pair.Value))
                {
                    if (player.loadedChunk.ContainsKey(pair.Key)) continue;

                    Chunk chunk = null;
                    try
                    {
                        chunk = this.GetChunk(pair.Key);

                        if (!this.chunks.ContainsKey(pair.Key))
                        {
                            this.chunks.Add(pair.Key, chunk);
                        }
                        player.loadedChunk.Add(pair.Key, pair.Value);
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
            lock (this.chunks)
            {
                foreach (Tuple<int, int> chunkKey in this.chunks.Keys.ToArray())
                {
                    if (!player.loadedChunk.ContainsKey(chunkKey))
                    {
                        if (!this.HasChunkLoadedByPlayer(chunkKey, player))
                        {
                            //this.Format.SetChunk(chunks[chunkKey]);//TODO:
                            this.chunks.Remove(chunkKey);
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

                bool r = players[i].loadedChunk.ContainsKey(chunkPos);
                if (r)
                {
                    return true;
                }
            }
            return false;
        }

        public void SendBlocks(Player[] players, Vector3[] vector3, int flags = UpdateBlockPacket.FLAG_NONE)
        {
            for (int i = 0; i < vector3.Length; ++i)
            {
                Block block = this.GetBlock(vector3[i]);
                UpdateBlockPacket pk = new UpdateBlockPacket();
                pk.Vector3 = vector3[i];
                pk.RuntimeId = block.RuntimeId;
                pk.Flags = flags;
                for (int j = 0; j < players.Length; ++j)
                {
                    players[j].SendPacket(pk);
                }
            }
        }

        public void UseItem(Vector3 pos, Item item, BlockFace blockFace, Vector3 clickPos, Player player)
        {
            Block clicked = this.GetBlock(pos);
            Block replace = clicked.GetSideBlock(blockFace);

            if (clicked.Y > 255 || clicked.Y < 0 || clicked.ID == BlockFactory.AIR)
            {
                return;
            }

            PlayerInteractEventArgs playerInteractEvent = new PlayerInteractEventArgs(player, item, clicked, blockFace);

            if (player.IsAdventure)
            {
                playerInteractEvent.IsCancel = true;
            }

            if (Server.ServerConfig.SpawnProtection > 0) //player.IsOp()
            {
                //TODO
            }

            PlayerEvents.OnPlayerInteract(playerInteractEvent);
            if (playerInteractEvent.IsCancel)
            {
                return;
            }

            clicked.Update(World.BLOCK_UPDATE_TOUCH);
            if (item.CanBeActivate && (!clicked.CanBeActivated || player.Sneaking) && item.Activate(player, this, clicked, blockFace, clickPos))
            {
                //TODO
            }

            if (!item.CanBePlace)
            {
                return;
            }
            Block hand = item.Block;
            hand.Position = replace.Position;

            if (clicked.CanBeReplaced)
            {
                replace = clicked;
                hand.Position = replace.Position;
            }

            //TODO : near by entity check

            //TODO : check can place on

            BlockPlaceEventArgs blockPlaceEvent = new BlockPlaceEventArgs(player, hand, replace, clicked, item);

            //TODO : check spawn protection

            BlockEvents.OnBlockPlace(blockPlaceEvent);
            if (blockPlaceEvent.IsCancel)
            {
                return;
            }
            hand.Place(clicked, replace, blockFace, clickPos, player, item);

            //TODO : block sound
        }

        public void UseBreak(Vector3 pos, Item item, Player player)
        {
            if (player.IsSpectator)
            {
                return;
            }
            Block block = this.GetBlock(pos);

            BlockBreakEventArgs blockBreakEvent = new BlockBreakEventArgs(player, block, item);

            if (player.IsSurvival && !block.CanBreak)
            {
                blockBreakEvent.IsCancel = true;
            }

            //TODO : spawn protection

            BlockEvents.OnBlockBreak(blockBreakEvent);
            if (blockBreakEvent.IsCancel)
            {
                return;
            }

            Item[] drops = blockBreakEvent.Drops;

            //TODO : can destroy

            //TODO : particle

            block.Break(player, item);

            item.Use(block);

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
    }
}
