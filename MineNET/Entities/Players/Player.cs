using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net;
using MineNET.Blocks;
using MineNET.Commands;
using MineNET.Data;
using MineNET.Entities.Attributes;
using MineNET.Inventories;
using MineNET.Items;
using MineNET.NBT.Data;
using MineNET.NBT.IO;
using MineNET.NBT.Tags;
using MineNET.Network;
using MineNET.Network.MinecraftPackets;
using MineNET.Network.RakNetPackets;
using MineNET.Text;
using MineNET.Values;
using MineNET.Worlds;
using MineNET.Worlds.Dimensions;

namespace MineNET.Entities.Players
{
    public partial class Player : EntityLiving, CommandSender
    {
        public override bool IsPlayer
        {
            get { return true; }
        }

        public override string Name { get; protected set; }
        public new string DisplayName { get; private set; }

        public IPEndPoint EndPoint { get; internal set; }

        public bool IsPreLogined { get; private set; }
        public bool IsLogined { get; private set; }
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

        public Player() : base(null, new CompoundTag())
        {
        }

        protected override void EntityInit()
        {
            base.EntityInit();

            this.Attributes.AddAttribute(EntityAttribute.HUNGER);
            this.Attributes.AddAttribute(EntityAttribute.SATURATION);
            this.Attributes.AddAttribute(EntityAttribute.EXHAUSTION);
            this.Attributes.AddAttribute(EntityAttribute.EXPERIENCE);
            this.Attributes.AddAttribute(EntityAttribute.EXPERIENCE_LEVEL);

            this.SetFlag(DATA_FLAGS, DATA_FLAG_BREATHING);
            this.SetFlag(DATA_FLAGS, DATA_FLAG_CAN_CLIMB);

            this.Inventory = new PlayerInventory(this);
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
            TextPacket pk = new TextPacket();
            pk.Type = TextPacket.TYPE_RAW;
            pk.Message = message;
            this.SendPacket(pk);
        }

        public void SendMessage(string message, params object[] args)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < args.Length; ++i)
            {
                list.Add(args[i].ToString());
            }

            TextPacket pk = new TextPacket();
            pk.Type = TextPacket.TYPE_TRANSLATION;
            pk.NeedsTranslation = true;
            pk.Message = message;
            pk.Parameters = list.ToArray();
            this.SendPacket(pk);
        }

        public void SendPlayStatus(int status, int flag = RakNetProtocol.FlagNormal)
        {
            PlayStatusPacket pk = new PlayStatusPacket();
            pk.Status = status;

            this.SendPacket(pk, flag: flag);
        }

        public void SendChunkRadiusUpdated(int radius)
        {
            ChunkRadiusUpdatedPacket pk = new ChunkRadiusUpdatedPacket();
            pk.Radius = radius;

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

        public void SendPacket(MinecraftPacket packet, int reliability = RakNetPacketReliability.RELIABLE,
            int flag = RakNetProtocol.FlagNormal)
        {
            NetworkSession session = Server.Instance.Network.GetSession(this.EndPoint);
            if (session == null)
            {
                return;
            }

            session.AddPacketBatchQueue(packet, reliability, flag);
        }

        public void SendAvailableCommands()
        {
            AvailableCommandsPacket availableCommandsPacket = new AvailableCommandsPacket();
            availableCommandsPacket.Commands = MineNET_Registries.Command.ToDictionary();
            this.SendPacket(availableCommandsPacket);
        }

        public void SendAllInventories()
        {
            this.Inventory.SendContents(this);
            this.Inventory.ArmorInventory.SendContents(this);
            this.Inventory.PlayerOffhandInventory.SendContents(this);
            this.Inventory.PlayerCursorInventory.SendContents(this);
            this.Inventory.OpendInventory?.SendContents(this);
        }

        internal override bool UpdateTick(long tick)
        {
            if (tick % 20 == 0 && this.AnySendChunk)
            {
                this.SendChunk();
            }

            return true;
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
            get { return (PlayerInventory) base.Inventory; }

            protected set { base.Inventory = value; }
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
            this.AdventureSettingsEntry.SetFlag(AdventureSettingsPacket.ALLOW_FLIGHT,
                this.IsCreative || this.IsSpectator);
            this.AdventureSettingsEntry.Update(this);
        }

        #endregion

        #region Init NBT

        public override void InitNBT()
        {
            CompoundTag tag = this.NamedTag;
            tag.PutInt("Dimension", DimensionIDs.OverWorld);
            tag.PutInt("PlayerGameType", Server.Instance.ServerProperty.GameMode.GetIndex());

            tag.PutInt("Score", 0);

            tag.PutInt("SelectedItemSlot", 0);
            tag.PutCompound("SelectedItem", NBTIO.WriteItem(new ItemStack(Item.Get(BlockIDs.AIR))));

            this.World = World.GetMainWorld();
            tag.PutString("LastWorldName", this.World.Name);

            tag.PutInt("SpawnX", this.World.SpawnX);
            tag.PutInt("SpawnY", this.World.SpawnY);
            tag.PutInt("SpawnZ", this.World.SpawnZ);

            tag.PutFloat("LastX", this.World.SpawnX);
            tag.PutFloat("LastY", this.World.SpawnY);
            tag.PutFloat("LastZ", this.World.SpawnZ);
        }

        #endregion

        #region Load & Save Method

        public void Load()
        {
            string path = $"{Server.PlayerDataPath}\\{this.LoginData.XUID}.dat";
            if (!File.Exists(path))
            {
                this.InitNBT();
                NBTIO.WriteGZIPFile(path, this.NamedTag, NBTEndian.BIG_ENDIAN);
            }
            else
            {
                this.NamedTag = NBTIO.ReadGZIPFile(path, NBTEndian.BIG_ENDIAN);
            }

            this.World = World.GetWorld(this.NamedTag.GetString("LastWorld")) ?? World.GetMainWorld();

            this.X = this.NamedTag.GetFloat("LastX");
            this.Y = this.NamedTag.GetFloat("LastY");
            this.Z = this.NamedTag.GetFloat("LastZ");

            this.SpawnX = this.NamedTag.GetInt("SpawnX");
            this.SpawnY = this.NamedTag.GetInt("SpawnY");
            this.SpawnZ = this.NamedTag.GetInt("SpawnZ");

            this.AdventureSettingsEntry = new AdventureSettingsEntry();

            this.GameMode = GameModeExtention.FromIndex(this.NamedTag.GetInt("PlayerGameType"));
        }

        public void Save()
        {
            this.NamedTag.PutInt("PlayerGameType", this.GameMode.GetIndex());

            this.NamedTag.PutString("LastWorldName", this.World.Name);

            this.NamedTag.PutInt("SpawnX", this.SpawnX);
            this.NamedTag.PutInt("SpawnY", this.SpawnY);
            this.NamedTag.PutInt("SpawnZ", this.SpawnZ);

            this.NamedTag.PutFloat("LastX", this.X);
            this.NamedTag.PutFloat("LastY", this.Y);
            this.NamedTag.PutFloat("LastZ", this.Z);

            string path = $"{Server.PlayerDataPath}\\{this.LoginData.XUID}.dat";
            NBTIO.WriteGZIPFile(path, this.NamedTag, NBTEndian.BIG_ENDIAN);
        }

        #endregion

        public bool CanInteract(Vector3 pos, double maxDistance)
        {
            if (Vector3.DistanceSquared((Vector3) this.Position, pos) > maxDistance * maxDistance)
            {
                return false;
            }

            Vector2 dv = this.DirectionPlane;
            float dot1 = Vector2.Dot(dv, new Vector2(this.X, this.Z));
            float dot2 = Vector2.Dot(dv, new Vector2(pos.X, this.Z));
            return (dot2 - dot1) >= -0.5;
        }
    }
}