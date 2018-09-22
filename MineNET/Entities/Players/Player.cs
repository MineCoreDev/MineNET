using MineNET.Blocks;
using MineNET.Commands;
using MineNET.Data;
using MineNET.Entities.Attributes;
using MineNET.Inventories;
using MineNET.Inventories.Transactions;
using MineNET.Inventories.Transactions.Action;
using MineNET.Inventories.Transactions.Data;
using MineNET.IO;
using MineNET.Items;
using MineNET.NBT.Tags;
using MineNET.Network;
using MineNET.Network.MinecraftPackets;
using MineNET.Network.RakNetPackets;
using MineNET.Text;
using MineNET.Values;
using MineNET.Worlds;
using MineNET.Worlds.Rule;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;

namespace MineNET.Entities.Players
{
    public class Player : EntityLiving, CommandSender
    {
        #region Property & Field

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

        public override float Width { get; } = 0.60f;
        public override float Height { get; } = 1.80f;

        public ConcurrentDictionary<Tuple<int, int>, double> LoadedChunks { get; private set; } =
            new ConcurrentDictionary<Tuple<int, int>, double>();

        private GameMode gameMode;

        #endregion

        #region Ctor

        public Player() : base(null, new CompoundTag())
        {
        }

        #endregion

        #region Init Method

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

        #endregion

        #region Send Message Method

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

        #endregion

        #region Send Status Method

        public void SendPlayStatus(int status, int flag = RakNetProtocol.FlagNormal)
        {
            PlayStatusPacket pk = new PlayStatusPacket();
            pk.Status = status;

            this.SendPacket(pk, flag: flag);
        }

        #endregion

        #region Send ChunkRadius Method

        public void SendChunkRadiusUpdated(int radius)
        {
            ChunkRadiusUpdatedPacket pk = new ChunkRadiusUpdatedPacket();
            pk.Radius = radius;

            this.SendPacket(pk);

            this.RequestChunkRadius = radius;
        }

        #endregion

        #region Send Chunk Method

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

        #endregion

        #region Send Packet Method

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

        #endregion

        #region Send AvailableCommands Method

        public void SendAvailableCommands()
        {
            AvailableCommandsPacket availableCommandsPacket = new AvailableCommandsPacket();
            availableCommandsPacket.Commands = MineNET_Registries.Command.ToDictionary();
            this.SendPacket(availableCommandsPacket);
        }

        #endregion

        #region Send Inventory Method

        public void SendAllInventories()
        {
            this.Inventory.SendContents(this);
            this.Inventory.ArmorInventory.SendContents(this);
            this.Inventory.PlayerOffhandInventory.SendContents(this);
            this.Inventory.PlayerCursorInventory.SendContents(this);
            this.Inventory.OpendInventory?.SendContents(this);
        }

        #endregion

        #region Update Method

        internal override bool UpdateTick(long tick)
        {
            if (tick % 20 == 0 && this.AnySendChunk)
            {
                this.SendChunk();
            }

            return true;
        }

        #endregion

        #region Packet Handle Method

        public void OnPacketHandle(MinecraftPacket packet)
        {
            if (packet is LoginPacket) //0x01
            {
                this.HandleLoginPacket((LoginPacket) packet);
            }
            else if (packet is ResourcePackClientResponsePacket) //0x08
            {
                this.HandleResourcePackClientResponsePacket((ResourcePackClientResponsePacket) packet);
            }
            else if (packet is MovePlayerPacket) //0x13
            {
                this.HandleMovePlayerPacket((MovePlayerPacket) packet);
            }
            else if (packet is InventoryTransactionPacket) //0x1e
            {
                this.HandleInventoryTransactionPacket((InventoryTransactionPacket) packet);
            }
            else if (packet is BlockPickRequestPacket)//0x22
            {
                this.HandleBlockPickRequestPacket((BlockPickRequestPacket) packet);
            }
            else if (packet is RequestChunkRadiusPacket) //0x45
            {
                this.HandleRequestChunkRadiusPacket((RequestChunkRadiusPacket) packet);
            }
            else if (packet is CommandRequestPacket)//0x4d
            {
                this.HandleCommandRequestPacket((CommandRequestPacket) packet);
            }
            else if (packet is SetLocalPlayerAsInitializedPacket) //0x70
            {
                this.HandleSetLocalPlayerAsInitializedPacket((SetLocalPlayerAsInitializedPacket) packet);
            }

            packet.Clone();
        }

        //0x01
        private void HandleLoginPacket(LoginPacket pk)
        {
            if (this.IsPreLogined)
            {
                return;
            }

            if (!pk.Result)
            {
                this.Close("disconnectionScreen.outdatedClient");
                return;
            }

            if (pk.Protocol < MinecraftProtocol.ClientProtocol)
            {
                this.SendPlayStatus(PlayStatusPacket.LOGIN_FAILED_CLIENT, RakNetProtocol.FlagImmediate);
                return;
            }
            else if (pk.Protocol > MinecraftProtocol.ClientProtocol)
            {
                this.SendPlayStatus(PlayStatusPacket.LOGIN_FAILED_SERVER, RakNetProtocol.FlagImmediate);
                return;
            }

            Player[] players = Server.Instance.GetPlayers();
            for (int i = 0; i < players.Length; ++i)
            {
                if (players[i].GetHashCode() != this.GetHashCode())
                {
                    if (players[i].Name == this.Name)
                    {
                        this.Close("disconnectionScreen.loggedinOtherLocation");
                        return;
                    }
                }
            }

            int maxplayers = Server.Instance.ServerProperty.MaxPlayers;
            if (players.Length > maxplayers)
            {
                this.SendPlayStatus(PlayStatusPacket.LOGIN_FAILED_SERVER_FULL, RakNetProtocol.FlagImmediate);
                //this.Close("disconnectionScreen.outdatedServer");
            }

            //TODO: Auth MS Server

            this.LoginData = pk.LoginData;
            this.Name = pk.LoginData.DisplayName;
            this.DisplayName = this.Name;
            this.Uuid = this.LoginData.ClientUUID;

            this.ClientData = pk.ClientData;
            this.Skin = this.ClientData.Skin;

            //TODO: Event

            this.IsPreLogined = true;

            this.SendPlayStatus(PlayStatusPacket.LOGIN_SUCCESS);

            ResourcePacksInfoPacket info = new ResourcePacksInfoPacket();
            this.SendPacket(info);
        }

        //0x08
        private void HandleResourcePackClientResponsePacket(ResourcePackClientResponsePacket pk)
        {
            if (this.PackSyncCompleted)
            {
                return;
            }

            if (pk.ResponseStatus == ResourcePackClientResponsePacket.STATUS_REFUSED)
            {
                this.Close("disconnectionScreen.resourcePack");
            }
            else if (pk.ResponseStatus == ResourcePackClientResponsePacket.STATUS_SEND_PACKS)
            {
                //TODO: ResourcePackDataInfoPacket
            }
            else if (pk.ResponseStatus == ResourcePackClientResponsePacket.STATUS_HAVE_ALL_PACKS)
            {
                ResourcePackStackPacket resourcePackStackPacket = new ResourcePackStackPacket();
                this.SendPacket(resourcePackStackPacket);

                this.HaveAllPacks = true;
            }
            else if (pk.ResponseStatus == ResourcePackClientResponsePacket.STATUS_COMPLETED && this.HaveAllPacks)
            {
                if (this.IsLogined)
                {
                    return;
                }

                //TODO: Event

                this.IsLogined = true;

                //TODO: Load NBT

                this.World = World.GetMainWorld();
                this.X = 128;
                this.Y = 6;
                this.Z = 128;

                StartGamePacket startGamePacket = new StartGamePacket();
                startGamePacket.EntityUniqueId = this.EntityID;
                startGamePacket.EntityRuntimeId = this.EntityID;
                startGamePacket.PlayerGamemode = this.GameMode;
                startGamePacket.PlayerPosition = new Vector3(this.X, this.Y, this.Z);
                startGamePacket.Direction = new Vector2(this.Yaw, this.Pitch);

                startGamePacket.WorldGamemode = this.World.Gamemode;
                startGamePacket.Difficulty = this.World.Difficulty;
                startGamePacket.SpawnX = this.World.SpawnX;
                startGamePacket.SpawnY = 5; //TODO: Safe Spawn
                startGamePacket.SpawnZ = this.World.SpawnZ;
                startGamePacket.WorldName = this.World.Name;

                startGamePacket.GameRules = new GameRules();
                startGamePacket.GameRules.Add(new GameRule<bool>("ShowCoordinates", true));
                this.SendPacket(startGamePacket);

                this.SendAvailableCommands();

                this.SendPlayStatus(PlayStatusPacket.PLAYER_SPAWN);
                this.HasSpawned = true;

                this.PlayerListEntry = new PlayerListEntry(this.LoginData.ClientUUID)
                {
                    EntityUniqueId = this.EntityID,
                    Name = this.DisplayName,
                    PlatForm = this.ClientData.DeviceOS,
                    Skin = this.Skin,
                    UUID = this.Uuid,
                    XboxUserId = this.LoginData.XUID
                };
                this.PlayerListEntry.UpdateAll();

                AdventureSettingsEntry adventureSettingsEntry = new AdventureSettingsEntry();
                adventureSettingsEntry.SetFlag(AdventureSettingsPacket.WORLD_IMMUTABLE, false);
                adventureSettingsEntry.SetFlag(AdventureSettingsPacket.NO_PVP, false);
                adventureSettingsEntry.SetFlag(AdventureSettingsPacket.AUTO_JUMP, false);
                adventureSettingsEntry.SetFlag(AdventureSettingsPacket.ALLOW_FLIGHT, true);
                adventureSettingsEntry.SetFlag(AdventureSettingsPacket.NO_CLIP, false);
                adventureSettingsEntry.SetFlag(AdventureSettingsPacket.FLYING, false);
                adventureSettingsEntry.CommandPermission =
                    PlayerPermissions.OPERATOR; //this.Op ? PlayerPermissions.OPERATOR : PlayerPermissions.MEMBER;
                adventureSettingsEntry.PlayerPermission =
                    PlayerPermissions.OPERATOR; //this.Op ? PlayerPermissions.OPERATOR : PlayerPermissions.MEMBER;
                adventureSettingsEntry.EntityUniqueId = this.EntityID;
                this.AdventureSettingsEntry = adventureSettingsEntry;
                this.AdventureSettingsEntry.Update(this);

                this.Attributes.Update(this);
                this.SendDataProperties();
            }
        }

        //0x13
        private void HandleMovePlayerPacket(MovePlayerPacket pk)
        {
            Vector3 pos = pk.Position;
            Vector3 direction = pk.Direction;
            //if ((Vector3) this.X != pos || this.Direction != direction)
            //{
            //this.SendPacketViewers(pk.Clone());
            //}
            this.X = pos.X;
            this.Y = pos.Y;
            this.Z = pos.Z;
            this.Pitch = direction.X;
            this.Yaw = direction.Y;
        }

        //0x1e
        private void HandleInventoryTransactionPacket(InventoryTransactionPacket pk)
        {
            List<InventoryAction> actions = new List<InventoryAction>();
            for (int i = 0; i < pk.Actions.Length; ++i)
            {
                try
                {
                    InventoryAction action = pk.Actions[i].GetInventoryAction(this);
                    actions.Add(action);
                }
                catch (Exception e)
                {
                    Logger.Info($"Unhandled inventory action from {this.Name}: {e.Message}");
                    this.SendAllInventories();
                    return;
                }
            }
            if (pk.TransactionType == InventoryTransactionPacket.TYPE_NORMAL)
            {
                InventoryTransaction transaction = new InventoryTransaction(this, actions);
                if (this.IsSpectator)
                {
                    this.SendAllInventories();
                    return;
                }
                if (!transaction.Execute())
                {
                    Logger.Info($"Failed to execute inventory transaction from {this.Name} with actions");
                }
            }
            else if (pk.TransactionType == InventoryTransactionPacket.TYPE_MISMATCH)
            {
                this.SendAllInventories();
                return;
            }
            else if (pk.TransactionType == InventoryTransactionPacket.TYPE_USE_ITEM)
            {
                UseItemData data = (UseItemData) pk.TransactionData;
                BlockCoordinate3D blockPos = data.BlockPos;
                BlockFace face = data.Face;

                if (data.ActionType == InventoryTransactionPacket.USE_ITEM_ACTION_CLICK_BLOCK)
                {
                    this.SetFlag(Entity.DATA_FLAGS, Entity.DATA_FLAG_ACTION, false, true);
                    if (this.CanInteract(blockPos + new Vector3(0.5f, 0.5f, 0.5f), this.IsCreative ? 13 : 7))
                    {
                        ItemStack item = this.Inventory.MainHandItem;
                        this.World.UseItem(blockPos, item, face, data.ClickPos, this);
                    }

                    //Send MainHand

                }
                else if (data.ActionType == InventoryTransactionPacket.USE_ITEM_ACTION_BREAK_BLOCK)
                {
                    ItemStack item = this.Inventory.MainHandItem;
                    if (this.CanInteract(blockPos + new Vector3(0.5f, 0.5f, 0.5f), this.IsCreative ? 13 : 7))
                    {
                        this.World.UseBreak(data.BlockPos, item, this);
                        if (this.IsSurvival)
                        {
                            //TODO : food
                            this.Inventory.SendMainHand();
                        }
                    }
                    else
                    {
                        this.World.SendBlocks(new Player[] { this }, new Vector3[] { data.BlockPos });
                    }
                }
            }
            else if (pk.TransactionType == InventoryTransactionPacket.TYPE_USE_ITEM_ON_ENTITY)
            {

            }
            else if (pk.TransactionType == InventoryTransactionPacket.TYPE_RELEASE_ITEM)
            {

            }
        }

        //0x22
        private void HandleBlockPickRequestPacket(BlockPickRequestPacket pk)
        {
            if (!this.IsCreative)
            {
                return;
            }
            Block block = this.World.GetBlock(pk.Position);
            ItemStack item = new ItemStack(block); //TODO : block entity nbt
            /*PlayerBlockPickRequestEventArgs args = new PlayerBlockPickRequestEventArgs(this, item);
            PlayerEvents.OnPlayerBlockPickRequest(args);
            if (args.IsCancel)
            {
                return;
            }*/
            List<int> air = new List<int>();
            for (int i = 0; i < pk.HotbarSlot; ++i)
            {
                ItemStack slot = this.Inventory.GetItem(i);
                if (slot == item)
                {
                    this.Inventory.MainHandSlot = i;
                    this.Inventory.SendMainHand(this);
                    return;
                }
                if (slot.Item.ID == BlockIDs.AIR)
                {
                    air.Add(i);
                }
            }
            if (air.Count == 0 || this.Inventory.MainHandItem.Item.ID == BlockIDs.AIR)
            {
                this.Inventory.MainHandItem = item;
                this.Inventory.SendMainHand(this);
                return;
            }
            this.Inventory.MainHandSlot = air[0];
            this.Inventory.MainHandItem = item;
            this.Inventory.SendMainHand(this);
        }


        //0x45
        private void HandleRequestChunkRadiusPacket(RequestChunkRadiusPacket pk)
        {
            int request = pk.Radius;
            int max = Server.Instance.ServerProperty.MaxViewDistance;

            Logger.Debug("%server.player.requestChunkRadius", this.DisplayName, request);
            if (request > max)
            {
                Logger.Debug("%server.player.updateChunkRadius", this.DisplayName, request, max);
                this.SendChunkRadiusUpdated(max);
            }
            else
            {
                this.SendChunkRadiusUpdated(request);
            }

            this.AnySendChunk = true;
        }

        //0x4d
        public void HandleCommandRequestPacket(CommandRequestPacket pk)
        {
            string command = pk.Command.Remove(0, 1);
            CommandData data = new CommandData(this, command);
            Server.Instance.Command.CommandHandler.OnCommandExecute(data);
        }

        //0x70
        public void HandleSetLocalPlayerAsInitializedPacket(SetLocalPlayerAsInitializedPacket pk)
        {
        }

        #endregion

        #region Close Player Method

        public void Close(string reason)
        {
            if (!string.IsNullOrEmpty(reason))
            {
                DisconnectPacket pk = new DisconnectPacket();
                pk.Message = reason;

                this.SendPacket(pk, flag: RakNetProtocol.FlagImmediate);
            }

            Server.Instance.Network.GetSession(this.EndPoint)?.Disconnect(reason);
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
            get
            {
                return this.gameMode;
            }

            set
            {
                this.gameMode = value;
                this.SendGameMode();
            }
        }

        public bool IsSurvival
        {
            get
            {
                return this.GameMode == GameMode.Survival;
            }
        }

        public bool IsCreative
        {
            get
            {
                return this.GameMode == GameMode.Creative;
            }
        }

        public bool IsAdventure
        {
            get
            {
                return this.GameMode == GameMode.Adventure;
            }
        }

        public bool IsSpectator
        {
            get
            {
                return this.GameMode == GameMode.Spectator;
            }
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

        }

        public void Save()
        {

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