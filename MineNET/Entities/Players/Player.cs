using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using MineNET.Commands;
using MineNET.Data;
using MineNET.Entities.Attributes;
using MineNET.Inventories;
using MineNET.NBT.Tags;
using MineNET.Network;
using MineNET.Network.MinecraftPackets;
using MineNET.Network.RakNetPackets;
using MineNET.Text;
using MineNET.Values;
using MineNET.Worlds;

namespace MineNET.Entities.Players
{
    public partial class Player : EntityLiving, CommandSender
    {
        public override bool IsPlayer
        {
            get { return true; }
        }

        public override int NetworkId { get; } = EntityIDs.PLAYER;

        public override string Name { get; protected set; }
        public new string DisplayName { get; private set; }

        public IPEndPoint EndPoint { get; internal set; }

        public bool IsPreLogined { get; private set; }
        public bool IsLoggedIn { get; private set; }
        public LoginData LoginData { get; private set; }
        public ClientData ClientData { get; private set; }
        public Skin Skin { get; private set; }
        public UUID Uuid { get; private set; }

        public PlayerListEntry PlayerListEntry { get; private set; }
        public AdventureSettingsEntry AdventureSettingsEntry { get; private set; }

        public bool PackSyncCompleted { get; private set; }
        public bool HaveAllPacks { get; private set; }

        public bool HasSpawned { get; private set; }
        public bool AnySendChunk { get; private set; }
        public int RequestChunkRadius { get; private set; } = 8;

        public int SpawnX { get; set; }
        public int SpawnY { get; set; }
        public int SpawnZ { get; set; }

        public override float Width { get; } = 0.60f;
        public override float Height { get; } = 1.80f;

        public ConcurrentDictionary<Tuple<int, int>, double> LoadedChunks { get; private set; } =
            new ConcurrentDictionary<Tuple<int, int>, double>();

        private GameMode gameMode;

        public Player() : base(World.GetMainWorld().GetChunk(new Tuple<int, int>(128 >> 4, 128 >> 4)), null)
        {
        }

        protected override void EntityInit(CompoundTag nbt)
        {
            base.EntityInit(nbt);

            this.Attributes.AddAttribute(EntityAttribute.HUNGER);
            this.Attributes.AddAttribute(EntityAttribute.SATURATION);
            this.Attributes.AddAttribute(EntityAttribute.EXHAUSTION);
            this.Attributes.AddAttribute(EntityAttribute.EXPERIENCE);
            this.Attributes.AddAttribute(EntityAttribute.EXPERIENCE_LEVEL);

            this.SetFlag(DATA_FLAGS, DATA_FLAG_BREATHING);
            this.SetFlag(DATA_FLAGS, DATA_FLAG_CAN_CLIMB);

            this.Inventory = new PlayerInventory(this);
            this.Inventory.LoadNBT(nbt);

            this.World = World.GetWorld(nbt.GetString("World")) ?? World.GetMainWorld();

            this.SpawnX = nbt.GetInt("SpawnX");
            this.SpawnY = nbt.GetInt("SpawnY");
            this.SpawnZ = nbt.GetInt("SpawnZ");

            this.AdventureSettingsEntry = new AdventureSettingsEntry();

            this.GameMode = GameModeExtention.FromIndex(nbt.GetInt("PlayerGameType"));
        }

        public void SendMessage(TranslationContainer message)
        {
            if (message.Args == null)
            {
                this.SendMessage(message.GetText(), new object[0]);
            }
            else
            {
                this.SendMessage(message.GetText(), message.Args);
            }
        }

        public void SendMessage(string message)
        {
            TextPacket pk = new TextPacket
            {
                Type = TextPacket.TYPE_RAW,
                Message = message
            };
            this.SendPacket(pk);
        }

        public void SendMessage(string message, params object[] args)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < args.Length; ++i)
            {
                list.Add(args[i].ToString());
            }

            TextPacket pk = new TextPacket
            {
                Type = TextPacket.TYPE_TRANSLATION,
                NeedsTranslation = true,
                Message = message,
                Parameters = list.ToArray()
            };
            this.SendPacket(pk);
        }

        public void SendPlayStatus(int status, int flag = RakNetProtocol.FlagNormal)
        {
            PlayStatusPacket pk = new PlayStatusPacket
            {
                Status = status
            };

            this.SendPacket(pk, flag: flag);
        }

        public void SendChunkRadiusUpdated(int radius)
        {
            ChunkRadiusUpdatedPacket pk = new ChunkRadiusUpdatedPacket
            {
                Radius = radius
            };

            this.SendPacket(pk);

            this.RequestChunkRadius = radius;
        }

        public void SendChunk()
        {
            //Task.Run(() =>
            //{
            //    Thread.CurrentThread.Name = "ChunkSendThread";
            foreach (Chunk c in this.World.LoadChunks(this, this.RequestChunkRadius))
            {
                c.SendChunk(this);
            }

            //});
        }

        public void SendPacket(MinecraftPacket packet, int reliability = RakNetPacketReliability.RELIABLE, int flag = RakNetProtocol.FlagNormal)
        {
            NetworkSession session = this.GetSession();
            if (session == null)
            {
                return;
            }

            session.AddPacketBatchQueue(packet, reliability, flag);
        }

        public void SendPacketViewers(MinecraftPacket packet)
        {
            Player[] players = this.Viewers;
            for (int i = 0; i < players.Length; ++i)
            {
                players[i].SendPacket(packet);
            }
        }

        public void SendAvailableCommands()
        {
            AvailableCommandsPacket availableCommandsPacket = new AvailableCommandsPacket
            {
                Commands = MineNET_Registries.Command.ToDictionary()
            };
            this.SendPacket(availableCommandsPacket);
        }

        public void SendAllInventories()
        {
            this.Inventory.SendContents(this);
            this.Inventory.ArmorInventory.SendContents(this);
            this.Inventory.EntityOffhandInventory.SendContents(this);
            this.Inventory.PlayerCursorInventory.SendContents(this);
            this.Inventory.OpendInventory?.SendContents(this);
        }

        public override void SendSpawnPacket(Player player)
        {
            AddPlayerPacket pk = new AddPlayerPacket
            {
                Uuid = this.Uuid,
                Username = this.Name,
                EntityUniqueId = this.EntityID,
                EntityRuntimeId = this.EntityID,
                Position = this.GetVector3(),
                Motion = new Vector3(),
                Direction = new Vector3(this.Yaw, this.Pitch, this.HeadYaw),
                Metadata = this.DataProperties
            };

            player.SendPacket(pk);
        }

        internal override bool UpdateTick(long tick)
        {
            if (tick % 20 == 0 && this.AnySendChunk)
            {
                this.SendChunk();
            }

            return true;
        }

        public bool IsOnline
        {
            get
            {
                return this.GetSession() != null && this.IsLoggedIn;
            }
        }

        public NetworkSession GetSession()
        {
            return Server.Instance.Network.GetSession(this.EndPoint);
        }

        #region Close Player Method

        public void Close(string reason)
        {
            if (!string.IsNullOrEmpty(reason))
            {
                DisconnectPacket pk = new DisconnectPacket();
                pk.Message = reason;

                this.SendPacket(pk, flag: RakNetProtocol.FlagImmediate);
            }

            this.Close();

            Server.Instance.Network.GetSession(this.EndPoint)?.Disconnect(reason);
        }

        public override void Close()
        {
            if (this.HasSpawned)
            {
                this.Save();
            }

            this.World?.UnLoadChunks(this);

            this.Closed = true;
        }

        #endregion

        public new PlayerInventory Inventory
        {
            get
            {
                return (PlayerInventory) base.Inventory;
            }

            protected set
            {
                base.Inventory = value;
            }
        }

        #region Gamemode Property

        public GameMode GameMode
        {
            get { return this.gameMode; }

            set
            {
                this.gameMode = value;
                this.SendGameMode();
            }
        }

        public bool IsSurvival
        {
            get { return this.GameMode == GameMode.Survival; }
        }

        public bool IsCreative
        {
            get { return this.GameMode == GameMode.Creative; }
        }

        public bool IsAdventure
        {
            get { return this.GameMode == GameMode.Adventure; }
        }

        public bool IsSpectator
        {
            get { return this.GameMode == GameMode.Spectator; }
        }

        public void SendGameMode()
        {
            SetPlayerGameTypePacket pk = new SetPlayerGameTypePacket();
            pk.GameMode = this.GameMode;
            this.SendPacket(pk);

            this.AdventureSettingsEntry.SetFlag(AdventureSettingsPacket.BUILD_AND_MINE, !this.IsSpectator);
            this.AdventureSettingsEntry.SetFlag(AdventureSettingsPacket.WORLD_BUILDER, !this.IsSpectator);
            this.AdventureSettingsEntry.SetFlag(AdventureSettingsPacket.NO_CLIP, this.IsSpectator);
            this.AdventureSettingsEntry.SetFlag(AdventureSettingsPacket.WORLD_IMMUTABLE, this.IsSpectator);
            this.AdventureSettingsEntry.SetFlag(AdventureSettingsPacket.NO_PVP, this.IsSpectator);
            this.AdventureSettingsEntry.SetFlag(AdventureSettingsPacket.FLYING, this.IsCreative || this.IsSpectator);
            this.AdventureSettingsEntry.SetFlag(AdventureSettingsPacket.ALLOW_FLIGHT, this.IsCreative || this.IsSpectator);
            this.AdventureSettingsEntry.Update(this);
        }

        #endregion

        #region Load & Save Method

        public void Load()
        {
            CompoundTag nbt = Server.Instance.GetOfflinePlayerData(this.LoginData.XUID);

            this.EntityInit(nbt);
        }

        public void Save()
        {
            CompoundTag nbt = this.SaveNBT();

            nbt.PutInt("PlayerGameType", this.GameMode.GetIndex());

            nbt.PutString("World", this.World.Name);

            nbt.PutInt("SpawnX", this.SpawnX);
            nbt.PutInt("SpawnY", this.SpawnY);
            nbt.PutInt("SpawnZ", this.SpawnZ);

            Dictionary<string, Tag> tags = this.Inventory.SaveNBT().Tags;
            foreach (string name in tags.Keys)
            {
                nbt.PutTag(name, tags[name]);
            }

            Server.Instance.SaveOfflinePlayerData(this.LoginData.XUID, nbt);
        }

        #endregion

        public bool CanInteract(Vector3 pos, double maxDistance)
        {
            if (Vector3.DistanceSquared(this.GetVector3(), pos) > maxDistance * maxDistance)
            {
                return false;
            }

            Vector2 dv = this.GetDirectionPlane();
            float dot1 = Vector2.Dot(dv, new Vector2(this.X, this.Z));
            float dot2 = Vector2.Dot(dv, new Vector2(pos.X, this.Z));
            return (dot2 - dot1) >= -0.5;
        }
    }
}