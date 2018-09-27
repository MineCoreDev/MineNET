using System;
using System.Collections.Generic;
using MineNET.Blocks;
using MineNET.Commands;
using MineNET.Data;
using MineNET.Events.PlayerEvents;
using MineNET.Inventories.Transactions;
using MineNET.Inventories.Transactions.Action;
using MineNET.Inventories.Transactions.Data;
using MineNET.IO;
using MineNET.Items;
using MineNET.Network.MinecraftPackets;
using MineNET.Network.RakNetPackets;
using MineNET.Values;
using MineNET.Worlds.Rule;

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
            else if (packet is ResourcePackClientResponsePacket) //0x08
            {
                this.HandleResourcePackClientResponsePacket((ResourcePackClientResponsePacket) packet);
            }
            else if (packet is TextPacket) //0x09
            {
                this.HandleTextPacket((TextPacket) packet);
            }
            else if (packet is MovePlayerPacket) //0x13
            {
                this.HandleMovePlayerPacket((MovePlayerPacket) packet);
            }
            else if (packet is InventoryTransactionPacket) //0x1e
            {
                this.HandleInventoryTransactionPacket((InventoryTransactionPacket) packet);
            }
            else if (packet is MobEquipmentPacket) //0x1f
            {
                this.MobEquipmentHandle((MobEquipmentPacket) packet);
            }
            else if (packet is BlockPickRequestPacket) //0x22
            {
                this.HandleBlockPickRequestPacket((BlockPickRequestPacket) packet);
            }
            else if (packet is PlayerActionPacket) //0x24
            {
                this.HandlePlayerActionPacket((PlayerActionPacket) packet);
            }
            else if (packet is AnimatePacket) //0x2c
            {
                this.HandleAnimatePacket((AnimatePacket) packet);
            }
            else if (packet is RequestChunkRadiusPacket) //0x45
            {
                this.HandleRequestChunkRadiusPacket((RequestChunkRadiusPacket) packet);
            }
            else if (packet is CommandRequestPacket) //0x4d
            {
                this.HandleCommandRequestPacket((CommandRequestPacket) packet);
            }
            else if (packet is SetLocalPlayerAsInitializedPacket) //0x70
            {
                this.HandleSetLocalPlayerAsInitializedPacket((SetLocalPlayerAsInitializedPacket) packet);
            }

            packet.Dispose();
        }

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
                    Server.Instance.Event.Player.OnPlayerChant(this, args);
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

        #region MovePlayerPacket 0x13

        protected virtual void HandleMovePlayerPacket(MovePlayerPacket pk)
        {
            Vector3 pos = pk.Position;
            Vector3 direction = pk.Direction;
            this.X = pos.X;
            this.Y = pos.Y;
            this.Z = pos.Z;
            this.Pitch = direction.X;
            this.Yaw = direction.Y;

            this.SendPacketViewers(pk.Clone());
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

        #endregion

        #region MobEquipmentPacket 0x1f

        protected virtual void MobEquipmentHandle(MobEquipmentPacket pk)
        {
            this.Inventory.MainHandSlot = pk.HotbarSlot;
            this.SetFlag(DATA_FLAGS, DATA_FLAG_ACTION, false, true);
        }

        #endregion

        #region BlockPickRequestPacket 0x22

        protected virtual void HandleBlockPickRequestPacket(BlockPickRequestPacket pk)
        {
            if (!this.IsCreative)
            {
                return;
            }

            Block block = this.World.GetBlock(pk.Position);
            ItemStack item = new ItemStack(block); //TODO : block entity nbt
            PlayerBlockPickRequestEventArgs args = new PlayerBlockPickRequestEventArgs(this, item);
            Server.Instance.Event.Player.OnPlayerBlockPickRequest(this, args);
            if (args.IsCancel)
            {
                return;
            }

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
                    //this.AddExhaustion(0.8f, PlayerExhaustEventArgs.CAUSE_SPRINT_JUMPING);
                }
                else
                {
                    //this.AddExhaustion(0.2f, PlayerExhaustEventArgs.CAUSE_JUMPING);
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

        #region AnimatePacket 0x2c

        protected virtual void HandleAnimatePacket(AnimatePacket pk)
        {
            //Server.Instance.BroadcastPacket(pk, this.Viewers);
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

        #region CommandRequestPacket 0x4d

        protected virtual void HandleCommandRequestPacket(CommandRequestPacket pk)
        {
            string command = pk.Command.Remove(0, 1);
            CommandData data = new CommandData(this, command);
            Server.Instance.Command.CommandHandler.OnCommandExecute(data);
        }

        #endregion

        #region SetLocalPlayerAsInitializedPacket 0x70

        protected virtual void HandleSetLocalPlayerAsInitializedPacket(SetLocalPlayerAsInitializedPacket pk)
        {
        }

        #endregion

        #endregion
    }
}