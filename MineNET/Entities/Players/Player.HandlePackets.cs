using MineNET.Blocks;
using MineNET.Commands;
using MineNET.Data;
using MineNET.Events.EntityEvents;
using MineNET.Events.PlayerEvents;
using MineNET.Inventories;
using MineNET.Inventories.Transactions;
using MineNET.Inventories.Transactions.Action;
using MineNET.Inventories.Transactions.Data;
using MineNET.IO;
using MineNET.Items;
using MineNET.Network.MinecraftPackets;
using MineNET.Network.RakNetPackets;
using MineNET.Values;
using MineNET.Worlds.Rule;
using System;
using System.Collections.Generic;

namespace MineNET.Entities.Players
{
    public partial class Player
    {
        #region Packet Handle Method

        public void OnPacketHandle(MinecraftPacket packet)
        {
            if (packet is LoginPacket) //0x01
            {
                this.HandleLoginPacket((LoginPacket) packet);
            }
            else if (packet is PlayStatusPacket) //0x02
            {
                this.HandlePlayStatusPacket((PlayStatusPacket) packet);
            }
            else if (packet is ServerToClientHandshakePacket) //0x03
            {
                this.HandleServerToClientHandshakePacket((ServerToClientHandshakePacket) packet);
            }
            else if (packet is ClientToServerHandshakePacket) //0x04
            {
                this.HandleClientToServerHandshakePacket((ClientToServerHandshakePacket) packet);
            }
            else if (packet is ResourcePacksInfoPacket) //0x06
            {
                this.HandleResourcePacksInfoPacket((ResourcePacksInfoPacket) packet);
            }
            else if (packet is ResourcePackStackPacket) //0x07
            {
                this.HandleResourcePackStackPacket((ResourcePackStackPacket) packet);
            }
            else if (packet is ResourcePackClientResponsePacket) //0x08
            {
                this.HandleResourcePackClientResponsePacket((ResourcePackClientResponsePacket) packet);
            }
            else if (packet is TextPacket) //0x09
            {
                this.HandleTextPacket((TextPacket) packet);
            }
            else if (packet is SetTimePacket) //0x0a
            {
                this.HandleSetTimePacket((SetTimePacket) packet);
            }
            else if (packet is StartGamePacket) //0x0b
            {
                this.HandleStartGamePacket((StartGamePacket) packet);
            }
            else if (packet is AddPlayerPacket) //0x0c
            {
                this.HandleAddPlayerPacket((AddPlayerPacket) packet);
            }
            else if (packet is AddEntityPacket) //0x0d
            {
                this.HandleAddEntityPacket((AddEntityPacket) packet);
            }
            else if (packet is RemoveEntityPacket) //0x0e
            {
                this.HandleRemoveEntityPacket((RemoveEntityPacket) packet);
            }
            else if (packet is AddItemEntityPacket) //0x0f
            {
                this.HandleAddItemEntityPacket((AddItemEntityPacket) packet);
            }
            else if (packet is AddHangingEntityPacket) //0x10
            {
                this.HandleAddItemEntityPacket((AddItemEntityPacket) packet);
            }
            else if (packet is TakeItemEntityPacket) //0x11
            {
                this.HandleTakeItemEntityPacket((TakeItemEntityPacket) packet);
            }
            else if (packet is MoveEntityAbsolutePacket) //0x12
            {
                this.HandleMoveEntityAbsolutePacket((MoveEntityAbsolutePacket) packet);
            }
            else if (packet is MovePlayerPacket) //0x13
            {
                this.HandleMovePlayerPacket((MovePlayerPacket) packet);
            }
            else if (packet is RiderJumpPacket) //0x14
            {
                this.HandleRiderJumpPacket((RiderJumpPacket) packet);
            }
            else if (packet is UpdateBlockPacket) //0x15
            {
                this.HandleUpdateBlockPacket((UpdateBlockPacket) packet);
            }
            else if (packet is AddPaintingPacket) //0x16
            {
                this.HandleAddPaintingPacket((AddPaintingPacket) packet);
            }
            else if (packet is ExplodePacket) //0x17
            {
                this.HandleExplodePacket((ExplodePacket) packet);
            }
            else if (packet is LevelSoundEventPacket) //0x18
            {
                this.HandleLevelSoundPacket((LevelSoundEventPacket) packet);
            }
            else if (packet is LevelEventPacket) //0x19
            {
                this.HandleLevelEventPacket((LevelEventPacket) packet);
            }
            else if (packet is BlockEventPacket) //0x1a
            {
                this.HandleBlockEventPacket((BlockEventPacket) packet);
            }
            else if (packet is EntityEventPacket) //0x1b
            {
                this.HandleEntityEventPacket((EntityEventPacket) packet);
            }
            else if (packet is MobEffectPacket) //0x1c
            {
                this.HandleMobEffectPacket((MobEffectPacket) packet);
            }
            else if (packet is UpdateAttributesPacket) //0x1d
            {
                this.HandleUpdateAttributesPacket((UpdateAttributesPacket) packet);
            }
            else if (packet is InventoryTransactionPacket) //0x1e
            {
                this.HandleInventoryTransactionPacket((InventoryTransactionPacket) packet);
            }
            else if (packet is MobEquipmentPacket) //0x1f
            {
                this.MobEquipmentHandle((MobEquipmentPacket) packet);
            }
            else if (packet is MobArmorEquipmentPacket) //0x20
            {
                this.HandleMobArmorEquipmentPacket((MobArmorEquipmentPacket) packet);
            }
            else if (packet is InteractPacket) //0x21
            {
                this.HandleInteractPacket((InteractPacket) packet);
            }
            else if (packet is BlockPickRequestPacket) //0x22
            {
                this.HandleBlockPickRequestPacket((BlockPickRequestPacket) packet);
            }
            else if (packet is EntityPickRequestPacket) //0x23
            {
                this.HandleEntityPickRequestPacket((EntityPickRequestPacket) packet);
            }
            else if (packet is PlayerActionPacket) //0x24
            {
                this.HandlePlayerActionPacket((PlayerActionPacket) packet);
            }
            else if (packet is EntityFallPacket) //0x25
            {
                this.HandleEntityFallPacket((EntityFallPacket) packet);
            }
            else if (packet is HurtArmorPacket) //0x26
            {
                this.HandleHurtArmorPacket((HurtArmorPacket) packet);
            }
            else if (packet is SetEntityDataPacket) //0x27
            {
                this.HandleSetEntityDataPacket((SetEntityDataPacket) packet);
            }
            else if (packet is SetEntityMotionPacket) //0x28
            {
                this.HandleSetEntityMotionPacket((SetEntityMotionPacket) packet);
            }
            else if (packet is SetEntityLinkPacket) //0x29
            {
                this.HandleSetEntityLinkPacket((SetEntityLinkPacket) packet);
            }
            else if (packet is SetHealthPacket) //0x2a
            {
                this.HandleSetHealthPacket((SetHealthPacket) packet);
            }
            else if (packet is SetSpawnPositionPacket) //0x2b
            {
                this.HandleSetSpawnPositionPacket((SetSpawnPositionPacket) packet);
            }
            else if (packet is AnimatePacket) //0x2c
            {
                this.HandleAnimatePacket((AnimatePacket) packet);
            }
            else if (packet is RespawnPacket) //0x2d
            {
                this.HandleRespawnPacket((RespawnPacket) packet);
            }
            else if (packet is ContainerOpenPacket) //0x2e
            {
                this.HandleContainerOpenPacket((ContainerOpenPacket) packet);
            }
            else if (packet is ContainerClosePacket) //0x2f
            {
                this.HandleContainerClosePacket((ContainerClosePacket) packet);
            }
            //TODO
            else if (packet is RequestChunkRadiusPacket) //0x45
            {
                this.HandleRequestChunkRadiusPacket((RequestChunkRadiusPacket) packet);
            }
            else if (packet is CommandRequestPacket) //0x4d
            {
                this.HandleCommandRequestPacket((CommandRequestPacket) packet);
            }
            else if (packet is PlayerSkinPacket) //0x5d
            {
                this.HandlePlayerSkinPacket((PlayerSkinPacket) packet);
            }
            else if (packet is SetLocalPlayerAsInitializedPacket) //0x71
            {
                this.HandleSetLocalPlayerAsInitializedPacket((SetLocalPlayerAsInitializedPacket) packet);
            }

            packet.Dispose();
        }

        #endregion

        #region LoginPacket 0x01

        protected virtual void HandleLoginPacket(LoginPacket pk)
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

            PlayerPreLoginEventArgs args = new PlayerPreLoginEventArgs(this);
            Server.Instance.Event.Player.OnPlayerPreLogin(this, args);
            if (args.IsCancel)
            {
                this.Close(args.KickMessage);
                return;
            }


            this.IsPreLogined = true;

            this.SendPlayStatus(PlayStatusPacket.LOGIN_SUCCESS);

            ResourcePacksInfoPacket info = new ResourcePacksInfoPacket();
            this.SendPacket(info);
        }

        #endregion

        #region PlayStatusPacket 0x02

        protected virtual void HandlePlayStatusPacket(PlayStatusPacket pk)
        {

        }

        #endregion

        #region ServerToClientHandshakePacket 0x03

        protected virtual void HandleServerToClientHandshakePacket(ServerToClientHandshakePacket pk)
        {

        }

        #endregion

        #region ClientToServerHandshakePacket 0x04

        protected virtual void HandleClientToServerHandshakePacket(ClientToServerHandshakePacket pk)
        {

        }

        #endregion

        #region ResourcePacksInfoPacket 0x06

        protected virtual void HandleResourcePacksInfoPacket(ResourcePacksInfoPacket pk)
        {

        }

        #endregion

        #region ResourcePackStackPacket 0x07

        protected virtual void HandleResourcePackStackPacket(ResourcePackStackPacket pk)
        {

        }

        #endregion

        #region ResourcePackClientResponsePacket 0x08

        protected virtual void HandleResourcePackClientResponsePacket(ResourcePackClientResponsePacket pk)
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
                if (this.IsLoggedIn)
                {
                    return;
                }

                PlayerLoginEventArgs args = new PlayerLoginEventArgs(this);
                Server.Instance.Event.Player.OnPlayerLogin(this, args);
                if (args.IsCancel)
                {
                    this.Close(args.KickMessage);
                    return;
                }

                this.IsLoggedIn = true;

                this.Load();

                StartGamePacket startGamePacket = new StartGamePacket
                {
                    EntityUniqueId = this.EntityID,
                    EntityRuntimeId = this.EntityID,
                    PlayerGamemode = this.GameMode,
                    PlayerPosition = new Vector3(this.X, this.Y, this.Z),
                    Direction = new Vector2(this.Yaw, this.Pitch),

                    WorldGamemode = this.World.Gamemode,
                    Difficulty = this.World.Difficulty,
                    SpawnX = this.World.SpawnX,
                    SpawnY = this.World.SpawnY,
                    SpawnZ = this.World.SpawnZ,
                    WorldName = this.World.Name,

                    GameRules = new GameRules()
                };
                startGamePacket.GameRules.Add(new GameRule<bool>("ShowCoordinates", true));
                this.SendPacket(startGamePacket);

                this.SendPacket(new AvailableEntityIdentifiersPacket());

                this.SendAvailableCommands();

                this.SendPlayStatus(PlayStatusPacket.PLAYER_SPAWN);

                this.HasSpawned = true;

                this.PlayerListEntry = new PlayerListEntry(this.LoginData.ClientUUID)
                {
                    EntityUniqueId = this.EntityID,
                    Name = this.DisplayName,
                    Skin = this.Skin,
                    XboxUserId = this.LoginData.XUID
                };
                this.PlayerListEntry.UpdateAll();

                this.AdventureSettingsEntry.CommandPermission =
                    PlayerPermissions.OPERATOR; //this.Op ? PlayerPermissions.OPERATOR : PlayerPermissions.MEMBER;
                this.AdventureSettingsEntry.PlayerPermission =
                    PlayerPermissions.OPERATOR; //this.Op ? PlayerPermissions.OPERATOR : PlayerPermissions.MEMBER;
                this.AdventureSettingsEntry.EntityUniqueId = this.EntityID;
                this.AdventureSettingsEntry.Update(this);

                this.Attributes.Update(this);
                this.SendDataProperties();

                this.Inventory.SendContents(this);
                this.Inventory.SendMainHand(this);
                this.Inventory.ArmorInventory.SendContents(this);
                this.Inventory.SendCreativeItems();
            }
        }

        #endregion

        #region TextPacket 0x09

        protected virtual void HandleTextPacket(TextPacket pk)
        {
            if (pk.Type == TextPacket.TYPE_CHAT)
            {
                string message = pk.Message.Trim();
                if (message != "" && message.Length < 256)
                {
                    PlayerChatEventArgs args = new PlayerChatEventArgs(this, message);
                    Server.Instance.Event.Player.OnPlayerChat(this, args);
                    if (args.IsCancel)
                    {
                        return;
                    }

                    message = $"<{this.Name}§f> {args.Message}§f";
                    Server.Instance.BroadcastChat(message);
                }
            }
        }

        #endregion

        #region SetTimePacket 0x0a

        protected virtual void HandleSetTimePacket(SetTimePacket pk)
        {

        }

        #endregion

        #region StartGamePacket 0x0b

        protected virtual void HandleStartGamePacket(StartGamePacket pk)
        {

        }

        #endregion

        #region AddPlayerPacket 0x0c

        protected virtual void HandleAddPlayerPacket(AddPlayerPacket pk)
        {

        }

        #endregion

        #region AddEntityPacket 0x0d

        protected virtual void HandleAddEntityPacket(AddEntityPacket pk)
        {

        }

        #endregion

        #region RemoveEntityPacket 0x0e

        protected virtual void HandleRemoveEntityPacket(RemoveEntityPacket pk)
        {

        }

        #endregion

        #region AddItemEntityPacket 0x0f

        protected virtual void HandleAddItemEntityPacket(AddItemEntityPacket pk)
        {

        }

        #endregion

        #region AddHangingEntityPacket 0x10

        protected virtual void HandleAddHangingEntityPacket(AddHangingEntityPacket pk)
        {

        }

        #endregion

        #region TakeItemEntityPacket 0x11

        protected virtual void HandleTakeItemEntityPacket(TakeItemEntityPacket pk)
        {

        }

        #endregion

        #region MoveEntityAbsolutePacket 0x12

        protected virtual void HandleMoveEntityAbsolutePacket(MoveEntityAbsolutePacket pk)
        {

        }

        #endregion

        #region MovePlayerPacket 0x13

        protected virtual void HandleMovePlayerPacket(MovePlayerPacket pk)
        {
            Vector3 pos = pk.Position;
            Vector3 direction = pk.Direction;

            SetEntityMotionPacket packet = new SetEntityMotionPacket
            {
                EntityRuntimeId = this.EntityID,
                Motion = new Vector3(pos.X - this.X, pos.Y - this.Y, pos.Z - this.Z)
            };
            //this.SendPacketViewers(pk);

            this.X = pos.X;
            this.Y = pos.Y -= this.BaseOffset;
            this.Z = pos.Z;
            this.Pitch = direction.X;
            this.Yaw = direction.Y;
            this.HeadYaw = direction.Z;

            this.SendPacketViewers(pk);
        }

        #endregion

        #region RiderJumpPacket 0x14

        protected virtual void HandleRiderJumpPacket(RiderJumpPacket pk)
        {

        }

        #endregion

        #region UpdateBlockPacket 0x15

        protected virtual void HandleUpdateBlockPacket(UpdateBlockPacket pk)
        {

        }

        #endregion

        #region AddPaintingPacket 0x16

        protected virtual void HandleAddPaintingPacket(AddPaintingPacket pk)
        {

        }

        #endregion

        #region ExplodePacket 0x17

        protected virtual void HandleExplodePacket(ExplodePacket pk)
        {

        }

        #endregion

        #region LevelSoundPaclet 0x18

        protected virtual void HandleLevelSoundPacket(LevelSoundEventPacket pk)
        {
            this.SendPacketViewers(pk);
            this.SendPacket(pk);
        }

        #endregion

        #region LevelEventPacket 0x19

        protected virtual void HandleLevelEventPacket(LevelEventPacket pk)
        {

        }

        #endregion

        #region BlockEventPacket 0x1a

        protected virtual void HandleBlockEventPacket(BlockEventPacket pk)
        {

        }

        #endregion

        #region EntityEventPacket 0x1b

        protected virtual void HandleEntityEventPacket(EntityEventPacket pk)
        {
            this.SendPacket(pk);
            Server.Instance.BroadcastSendPacket(pk, this.Viewers);
        }

        #endregion

        #region MobEffectPacket 0x1c

        protected virtual void HandleMobEffectPacket(MobEffectPacket pk)
        {

        }

        #endregion

        #region UpdateAttributesPacket 0x1d

        protected virtual void HandleUpdateAttributesPacket(UpdateAttributesPacket pk)
        {

        }

        #endregion

        #region InventoryTransactionPacket 0x1e

        protected virtual void HandleInventoryTransactionPacket(InventoryTransactionPacket pk)
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
                    this.SendAllInventories();
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
                    this.SetFlag(DATA_FLAGS, DATA_FLAG_ACTION, false, true);
                    if (this.CanInteract(blockPos + new Vector3(0.5f, 0.5f, 0.5f), this.IsCreative ? 13 : 7))
                    {
                        ItemStack item = this.Inventory.MainHandItem;
                        this.World.UseItem(blockPos, item, face, data.ClickPos, this);
                    }

                    //Send MainHand
                }
                else if (data.ActionType == InventoryTransactionPacket.USE_ITEM_ACTION_CLICK_AIR)
                {

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
                ReleaseItemData data = (ReleaseItemData) pk.TransactionData;
                if (pk.TransactionData.ActionType == InventoryTransactionPacket.RELEASE_ITEM_ACTION_RELEASE)
                {
                    ItemStack stack = this.Inventory.MainHandItem;
                    Item item = stack.Item;
                    item.ReleaseUsing(this);
                }
                else if (pk.TransactionData.ActionType == InventoryTransactionPacket.RELEASE_ITEM_ACTION_CONSUME)
                {
                    if (this.Inventory.MainHandItem != data.ItemMainHand || this.Inventory.MainHandSlot != data.HotbarSlot)
                    {
                        this.Inventory.SendMainHand(this);
                        return;
                    }

                    ItemStack stack = this.Inventory.MainHandItem;
                    Item item = stack.Item;
                    if (!(item is IConsumeable))
                    {
                        this.Inventory.SendMainHand(this);
                        return;
                    }

                    IConsumeable consume = (IConsumeable) item;
                    PlayerItemConsumeEventArgs args = new PlayerItemConsumeEventArgs(this, stack, consume);
                    Server.Instance.Event.Player.OnPlayerItemConsume(this, args);
                    if (args.IsCancel)
                    {
                        this.Inventory.SendMainHand(this);
                        return;
                    }
                    consume.OnConsume(this, stack);
                }
            }
        }

        #endregion

        #region MobEquipmentPacket 0x1f

        protected virtual void MobEquipmentHandle(MobEquipmentPacket pk)
        {
            int hotbar = pk.HotbarSlot;
            PlayerItemHeldEventArgs args = new PlayerItemHeldEventArgs(this, pk.Item, this.Inventory.MainHandSlot, hotbar);
            Server.Instance.Event.Player.OnPlayerItemHeld(this, args);
            if (args.IsCancel)
            {
                this.Inventory.SendMainHand(this); //TODO?
                return;
            }

            this.Inventory.MainHandSlot = pk.HotbarSlot;
            this.SetFlag(DATA_FLAGS, DATA_FLAG_ACTION, false, true);
        }

        #endregion

        #region MobArmorEquipmentPacket 0x20

        protected virtual void HandleMobArmorEquipmentPacket(MobArmorEquipmentPacket pk)
        {

        }

        #endregion

        #region InteractPacket 0x21

        protected virtual void HandleInteractPacket(InteractPacket pk)
        {

        }

        #endregion

        #region BlockPickRequestPacket 0x22

        protected virtual void HandleBlockPickRequestPacket(BlockPickRequestPacket pk)
        {
            Block block = this.World.GetBlock(pk.Position);
            ItemStack item = new ItemStack(block); //TODO : block entity nbt
            bool requestData = pk.AddUserData;

            PlayerBlockPickRequestEventArgs args = new PlayerBlockPickRequestEventArgs(this, block, item, requestData);
            Server.Instance.Event.Player.OnPlayerBlockPickRequest(this, args);
            if (args.IsCancel)
            {
                return;
            }
            item = args.Item;

            PlayerInventory inventory = this.Inventory;
            List<int> air = new List<int>();

            for (int i = 0; i < pk.HotbarSlot; ++i)
            {
                ItemStack slot = inventory.GetItem(i);
                if (slot.Equals(item, true, false))
                {
                    inventory.MainHandSlot = i;
                    inventory.SendMainHand(this);
                    return;
                }

                if (slot.Item.ID == BlockIDs.AIR)
                {
                    air.Add(i);
                }
            }

            for (int i = 0; i < inventory.Size; ++i)
            {
                ItemStack check = inventory.GetItem(i);
                if (check.Equals(item, true, false))
                {
                    inventory.SetItem(i, inventory.MainHandItem);
                    inventory.MainHandItem = check;
                    inventory.SendMainHand(this);
                    return;
                }
            }
            if (this.IsCreative)
            {
                if (air.Count != 0)
                {
                    inventory.MainHandSlot = air[0];
                }
                inventory.AddItem(inventory.MainHandItem);
                inventory.MainHandItem = item;
                inventory.SendMainHand(this);
                return;
            }
        }

        #endregion

        #region EntityPickRequestPacket 0x23

        protected virtual void HandleEntityPickRequestPacket(EntityPickRequestPacket pk)
        {

        }

        #endregion

        #region PlayerActionPacket 0x24

        protected virtual void HandlePlayerActionPacket(PlayerActionPacket pk)
        {
            Vector3 pos = pk.Position;
            BlockFace face = pk.Face;
            if (pk.Action == PlayerActionPacket.ACTION_JUMP)
            {
                PlayerJumpEventArgs args = new PlayerJumpEventArgs(this);
                Server.Instance.Event.Player.OnPlayerJump(this, args);
                if (this.Sprinting)
                {
                    this.Exhaust(0.8f, PlayerExhaustEventArgs.CAUSE_SPRINT_JUMPING);
                }
                else
                {
                    this.Exhaust(0.2f, PlayerExhaustEventArgs.CAUSE_JUMPING);
                }
            }
            else if (pk.Action == PlayerActionPacket.ACTION_START_SPRINT)
            {
                PlayerToggleSprintEventArgs args = new PlayerToggleSprintEventArgs(this, true);
                Server.Instance.Event.Player.OnPlayerToggleSprint(this, args);
                if (args.IsCancel)
                {
                    this.SendDataProperties();
                }

                this.Sprinting = true;
                this.SendDataProperties();
            }
            else if (pk.Action == PlayerActionPacket.ACTION_STOP_SPRINT)
            {
                PlayerToggleSprintEventArgs args = new PlayerToggleSprintEventArgs(this, false);
                Server.Instance.Event.Player.OnPlayerToggleSprint(this, args);
                if (args.IsCancel)
                {
                    this.SendDataProperties();
                }

                this.Sprinting = false;
                this.SendDataProperties();
            }
            else if (pk.Action == PlayerActionPacket.ACTION_START_SNEAK)
            {
                PlayerToggleSneakEventArgs args = new PlayerToggleSneakEventArgs(this, true);
                Server.Instance.Event.Player.OnPlayerToggleSneak(this, args);
                if (args.IsCancel)
                {
                    this.SendDataProperties();
                }

                this.Sneaking = true;
                this.SendDataProperties();
            }
            else if (pk.Action == PlayerActionPacket.ACTION_STOP_SNEAK)
            {
                PlayerToggleSneakEventArgs args = new PlayerToggleSneakEventArgs(this, false);
                Server.Instance.Event.Player.OnPlayerToggleSneak(this, args);
                if (args.IsCancel)
                {
                    this.SendDataProperties();
                }

                this.Sneaking = false;
                this.SendDataProperties();
            }
            else if (pk.Action == PlayerActionPacket.ACTION_START_GLIDE)
            {
                PlayerToggleGlideEventArgs args = new PlayerToggleGlideEventArgs(this, true);
                Server.Instance.Event.Player.OnPlayerToggleGlide(this, args);
                if (args.IsCancel)
                {
                    this.SendDataProperties();
                }

                this.Gliding = true;
                this.SendDataProperties();
            }
            else if (pk.Action == PlayerActionPacket.ACTION_STOP_GLIDE)
            {
                PlayerToggleGlideEventArgs args = new PlayerToggleGlideEventArgs(this, false);
                Server.Instance.Event.Player.OnPlayerToggleGlide(this, args);
                if (args.IsCancel)
                {
                    this.SendDataProperties();
                }

                this.Gliding = false;
                this.SendDataProperties();
            }

            this.Action = false;
        }

        #endregion

        #region EntityFallPacket 0x25

        protected virtual void HandleEntityFallPacket(EntityFallPacket pk)
        {

        }

        #endregion

        #region HurtArmorPacket 0x26

        protected virtual void HandleHurtArmorPacket(HurtArmorPacket pk)
        {

        }

        #endregion

        #region SetEntityDataPacket 0x27

        protected virtual void HandleSetEntityDataPacket(SetEntityDataPacket pk)
        {

        }

        #endregion

        #region SetEntityMotionPacket 0x28

        protected virtual void HandleSetEntityMotionPacket(SetEntityMotionPacket pk)
        {

        }

        #endregion

        #region SetEntityLinkPacket 0x29

        protected virtual void HandleSetEntityLinkPacket(SetEntityLinkPacket pk)
        {

        }

        #endregion

        #region SetHealthPacket 0x2a

        protected virtual void HandleSetHealthPacket(SetHealthPacket pk)
        {

        }

        #endregion

        #region SetSpawnPositionPacket 0x2b

        protected virtual void HandleSetSpawnPositionPacket(SetSpawnPositionPacket pk)
        {

        }

        #endregion

        #region AnimatePacket 0x2c

        protected virtual void HandleAnimatePacket(AnimatePacket pk)
        {
            EntityAnimationEventArgs args = new EntityAnimationEventArgs(this, pk.Action);
            Server.Instance.Event.Entity.OnEntityAnimation(this, args);
            if (args.IsCancel)
            {
                return;
            }
            this.SendPacketViewers(pk);
        }

        #endregion

        #region RespawnPacket 0x2d

        protected virtual void HandleRespawnPacket(RespawnPacket pk)
        {

        }

        #endregion

        #region ContainerOpenPacket 0x2e

        protected virtual void HandleContainerOpenPacket(ContainerOpenPacket pk)
        {

        }

        #endregion

        #region ContainerClosePacket 0x2f

        protected virtual void HandleContainerClosePacket(ContainerClosePacket pk)
        {

        }

        #endregion

        #region PlayerHotbarPacket 0x30

        protected virtual void HandlePlayerHotbarPacket(PlayerHotbarPacket pk)
        {

        }

        #endregion

        #region InventoryContentPacket 0x31

        protected virtual void HandleInventoryContentPacket(InventoryContentPacket pk)
        {

        }

        #endregion

        #region InventorySlotPacket 0x32

        protected virtual void HandleInventorySlotPacket(InventorySlotPacket pk)
        {

        }

        #endregion

        #region ContainerSetDataPacket 0x33

        protected virtual void HandleContainerSetDataPacket(ContainerSetDataPacket pk)
        {

        }

        #endregion

        #region CraftingDataPacket 0x34

        protected virtual void HandleCraftingDataPacket(CraftingDataPacket pk)
        {

        }

        #endregion

        #region CraftingEventPacket 0x35

        protected virtual void HandleCraftingEventPacket(CraftingEventPacket pk)
        {

        }

        #endregion

        #region GuiDataPickItemPacket 0x36

        protected virtual void HandleGuiDataPickItemPacket(GuiDataPickItemPacket pk)
        {

        }

        #endregion

        #region AdventureSettingsPacket 0x37

        protected virtual void HandleAdventureSettingsPacket(AdventureSettingsPacket pk)
        {

        }

        #endregion

        #region BlockEntityDataPacket 0x38

        protected virtual void HandleBlockEntityDataPacket(BlockEntityDataPacket pk)
        {

        }

        #endregion

        #region PlayerInputPacket 0x39

        protected virtual void HandlePlayerInputPacket(PlayerInputPacket pk)
        {

        }

        #endregion

        #region FullChunkDataPacket 0x3a

        protected virtual void HandleFullChunkDataPacket(FullChunkDataPacket pk)
        {

        }

        #endregion

        #region SetCommandsEnabledPacket 0x3b

        protected virtual void HandleSetCommandsEnabledPacket(SetCommandsEnabledPacket pk)
        {

        }

        #endregion

        #region SetDifficultyPacket 0x3c

        protected virtual void HandleSetDifficultyPacket(SetDifficultyPacket pk)
        {

        }

        #endregion

        #region ChangeDimensionPacket 0x3d

        protected virtual void HandleChangeDimensionPacket(ChangeDimensionPacket pk)
        {

        }

        #endregion

        #region SetPlayerGameTypePacket 0x3e

        protected virtual void HandleSetPlayerGameTypePacket(SetPlayerGameTypePacket pk)
        {

        }

        #endregion

        #region PlayerListPacket 0x3f

        protected virtual void HandlePlayerListPacket(PlayerListPacket pk)
        {

        }

        #endregion

        #region SimpleEventPacket 0x40

        protected virtual void HandleSimpleEventPacket(SimpleEventPacket pk)
        {

        }

        #endregion

        #region EventPacket 0x41

        protected virtual void HandleEventPacket(EventPacket pk)
        {

        }

        #endregion

        #region SpawnExperienceOrbPacket 0x42

        protected virtual void HandleSpawnExperienceOrbPacket(SpawnExperienceOrbPacket pk)
        {

        }

        #endregion

        #region ClientboundMapItemDataPacket 0x43

        protected virtual void HandleClientboundMapItemDataPacket(ClientboundMapItemDataPacket pk)
        {

        }

        #endregion

        #region MapInfoRequestPacket 0x44

        protected virtual void HandleMapInfoRequestPacket(MapInfoRequestPacket pk)
        {

        }

        #endregion

        #region RequestChunkRadiusPacket 0x45

        protected virtual void HandleRequestChunkRadiusPacket(RequestChunkRadiusPacket pk)
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

        #endregion

        #region ChunkRadiusUpdatedPacket 0x46

        protected virtual void HandleChunkRadiusUpdatedPacket(ChunkRadiusUpdatedPacket pk)
        {

        }

        #endregion

        #region ItemFrameDropItemPacket 0x47

        protected virtual void HandleItemFrameDropItemPacket(ItemFrameDropItemPacket pk)
        {

        }

        #endregion

        #region GameRulesChangedPacket 0x48

        protected virtual void HandleGameRulesChangedPacket(GameRulesChangedPacket pk)
        {

        }

        #endregion

        #region CameraPacket 0x49

        protected virtual void HandleCameraPacket(CameraPacket pk)
        {

        }

        #endregion

        #region BossEventPacket 0x4a

        protected virtual void HandleBossEventPacket(BossEventPacket pk)
        {

        }

        #endregion

        #region ShowCreditsPacket 0x4b

        protected virtual void Handle(ShowCreditsPacket pk)
        {

        }

        #endregion

        #region AvailableCommandsPacket 0x4c

        protected virtual void HandleAvailableCommandsPacket(AvailableCommandsPacket pk)
        {

        }

        #endregion

        #region CommandRequestPacket 0x4d

        protected virtual void HandleCommandRequestPacket(CommandRequestPacket pk)
        {
            string command = pk.Command.Remove(0, 1);
            CommandData data = new CommandData(this, command);
            Server.Instance.Command.CommandHandler.OnCommandExecute(data);
        }

        #endregion

        #region CommandBlockUpdatePacket 0x4e

        protected virtual void HandleCommandBlockUpdatePacket(CommandBlockUpdatePacket pk)
        {

        }

        #endregion

        #region CommandOutputPacket 0x4f

        protected virtual void HandleCommandOutputPacket(CommandOutputPacket pk)
        {

        }

        #endregion

        #region UpdateTradePacket 0x50

        protected virtual void HandleUpdateTradePacket(UpdateTradePacket pk)
        {

        }

        #endregion

        #region UpdateEquipPacket 0x51

        protected virtual void HandleUpdateEquipPacket(UpdateEquipPacket pk)
        {

        }

        #endregion

        #region ResourcePackDataInfoPacket 0x52

        protected virtual void HandleResourcePackDataInfoPacket(ResourcePackDataInfoPacket pk)
        {

        }

        #endregion

        #region ResourcePackChunkDataPacket 0x53

        protected virtual void HandleResourcePackChunkDataPacket(ResourcePackChunkDataPacket pk)
        {

        }

        #endregion

        #region ResourcePackChunkRequestPacket 0x54

        protected virtual void HandleResourcePackChunkRequestPacket(ResourcePackChunkRequestPacket pk)
        {

        }

        #endregion

        #region TransferPacket 0x55

        protected virtual void HandleTransferPacket(TransferPacket pk)
        {

        }

        #endregion

        #region PlaySoundPacket 0x56

        protected virtual void HandlePlaySoundPacket(PlaySoundPacket pk)
        {

        }

        #endregion

        #region StopSoundPacket 0x57

        protected virtual void HandleStopSoundPacket(StopSoundPacket pk)
        {

        }

        #endregion

        #region SetTitlePacket 0x58

        protected virtual void HandleSetTitlePacket(SetTitlePacket pk)
        {

        }

        #endregion

        #region AddBehaviorTreePacket 0x59

        protected virtual void HandleAddBehaviorTreePacket(AddBehaviorTreePacket pk)
        {

        }

        #endregion

        #region StructureBlockUpdatePacket 0x5a

        protected virtual void HandleStructureBlockUpdatePacket(StructureBlockUpdatePacket pk)
        {

        }

        #endregion

        #region ShowStoreOfferPacket 0x5b

        protected virtual void HandleShowStoreOfferPacket(ShowStoreOfferPacket pk)
        {

        }

        #endregion

        #region PurchaseReceiptPacket 0x5c

        protected virtual void HandlePurchaseReceiptPacket(PurchaseReceiptPacket pk)
        {

        }

        #endregion

        #region PlayerSkinPacket 0x5d

        protected virtual void HandlePlayerSkinPacket(PlayerSkinPacket pk)
        {
            PlayerSkinChangeEventArgs args = new PlayerSkinChangeEventArgs(this, this.Skin, pk.Skin);
            Server.Instance.Event.Player.OnPlayerSkinChange(this, args);
            if (args.IsCancel)
            {
                PlayerSkinPacket packet = new PlayerSkinPacket
                {
                    Uuid = this.Uuid,
                    Skin = this.Skin
                };
                this.SendPacket(packet);
                return;
            }
            this.Skin = pk.Skin;

            this.SendPacket(pk);
            this.SendPacketViewers(pk);
        }

        #endregion

        #region SubClientLoginPacket 0x5e

        protected virtual void HandleSubClientLoginPacket(SubClientLoginPacket pk)
        {

        }

        #endregion

        #region WSConnectPacket 0x5f

        protected virtual void HandleWSConnectPacket(AutomationClientConnectPacket pk)
        {

        }

        #endregion

        #region SetLastHurtByPacket 0x60

        protected virtual void HandleSetLastHurtByPacket(SetLastHurtByPacket pk)
        {

        }

        #endregion

        #region BookEditPacket 0x61

        protected virtual void HandleBookEditPacket(BookEditPacket pk)
        {

        }

        #endregion

        #region NpcRequestPacket 0x62

        protected virtual void HandleNpcRequestPacket(NpcRequestPacket pk)
        {

        }

        #endregion

        #region PhotoTransferPacket 0x63

        protected virtual void HandlePhotoTransferPacket(PhotoTransferPacket pk)
        {

        }

        #endregion

        #region ModalFormRequestPacket 0x64

        protected virtual void HandleModalFormRequestPacket(ModalFormRequestPacket pk)
        {

        }

        #endregion

        #region ModalFormResponsePacket 0x65

        protected virtual void HandleModalFormResponsePacket(ModalFormResponsePacket pk)
        {

        }

        #endregion

        #region ServerSettingsRequestPacket 0x66

        protected virtual void HandleServerSettingsRequestPacket(ServerSettingsRequestPacket pk)
        {

        }

        #endregion

        #region ServerSettingsResponsePacket 0x67

        protected virtual void HandleServerSettingsResponsePacket(ServerSettingsResponsePacket pk)
        {

        }

        #endregion

        #region ShowProfilePacket 0x68

        protected virtual void HandleShowProfilePacket(ShowProfilePacket pk)
        {

        }

        #endregion

        #region SetDefaultGameTypePacket 0x69

        protected virtual void HandleSetDefaultGameTypePacket(SetDefaultGameTypePacket pk)
        {

        }

        #endregion

        #region RemoveObjectivePacket 0x6a

        protected virtual void HandleRemoveObjectivePacket(RemoveObjectivePacket pk)
        {

        }

        #endregion

        #region SetDisplayObjectivePacket 0x6b

        protected virtual void HandleSetDisplayObjectivePacket(SetDisplayObjectivePacket pk)
        {

        }

        #endregion

        #region SetScorePacket 0x6c

        protected virtual void HandleSetScorePacket(SetScorePacket pk)
        {

        }

        #endregion

        #region LabTablePacket 0x6d

        protected virtual void HandleLabTablePacket(LabTablePacket pk)
        {

        }

        #endregion

        #region UpdateBlockSyncedPacket 0x6e

        protected virtual void HandleUpdateBlockSyncedPacket(UpdateBlockSyncedPacket pk)
        {

        }

        #endregion

        #region MoveEntityDeltaPacket 0x6f

        protected virtual void HandleMoveEntityDeltaPacket(MoveEntityDeltaPacket pk)
        {

        }

        #endregion

        #region SetScoreboardIdentityPacket 0x70

        protected virtual void HandleSetScoreboardIdentityPacket(SetScoreboardIdentityPacket pk)
        {

        }

        #endregion

        #region SetLocalPlayerAsInitializedPacket 0x71

        protected virtual void HandleSetLocalPlayerAsInitializedPacket(SetLocalPlayerAsInitializedPacket pk)
        {
            Player[] players = this.World.GetPlayers();
            for (int i = 0; i < players.Length; ++i)
            {
                if (players[i].Name == this.Name)
                {
                    continue;
                }
                players[i].SpawnTo(this);
            }

            this.SpawnToAll();

            PlayerJoinEventArgs playerJoinEvent = new PlayerJoinEventArgs(this, $"§e{this.Name} が世界にやってきました");
            Server.Instance.Event.Player.OnPlayerJoin(this, playerJoinEvent);
            if (!string.IsNullOrEmpty(playerJoinEvent.JoinMessage))
            {
                Server.Instance.BroadcastMessage(playerJoinEvent.JoinMessage);
            }
            Logger.Info($"§e{this.Name} join the game");
        }

        #endregion

        #region UpdateSoftEnumPacket 0x72

        protected virtual void HandleUpdateSoftEnumPacket(UpdateSoftEnumPacket pk)
        {

        }

        #endregion

        #region NetworkStackLatencyPacket 0x73

        protected virtual void HandleNetworkStackLatencyPacket(NetworkStackLatencyPacket pk)
        {

        }

        #endregion
    }
}