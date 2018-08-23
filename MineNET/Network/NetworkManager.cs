using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using MineNET.Entities.Players;
using MineNET.Events.NetworkEvents;
using MineNET.Events.NetworkEvents.RakNet;
using MineNET.Events.PlayerEvents;
using MineNET.Network.MinecraftPackets;
using MineNET.Network.RakNetPackets;

namespace MineNET.Network
{
    public sealed class NetworkManager : IDisposable
    {
        #region Property & Field
        public UdpClient Client { get; private set; }

        public Thread ReceiveThread { get; private set; }
        public Thread UpdateThread { get; private set; }

        public bool IsRunNetwork { get; private set; }

        public long ServerID { get; } = MineNET.Utils.Random.CreateRandomID();

        public ConcurrentDictionary<string, NetworkSession> Sessions { get; } = new ConcurrentDictionary<string, NetworkSession>();
        public ConcurrentDictionary<string, Player> Players { get; set; } = new ConcurrentDictionary<string, Player>();
        #endregion

        #region Ctor
        public NetworkManager()
        {
            this.Init();
        }
        #endregion

        #region Init Method
        private void Init()
        {
            UdpClient client = Server.Instance.NetworkSocket.Socket;
            if (client != null)
            {
                this.Client = client;

                this.RegisterRakNetPackets();
                this.RegisterMinecraftPackets();

                this.IsRunNetwork = true;

                this.ReceiveThread = new Thread(this.ReceiveClock);
                this.ReceiveThread.Name = "PacketThread";
                this.ReceiveThread.Start();

                this.UpdateThread = new Thread(this.OnUpdate);
                this.UpdateThread.Name = "SessionUpdateThread";
                this.UpdateThread.Start();
            }
        }

        private void RegisterRakNetPackets()
        {
            MineNET_Registries.RakNetPacket.Add(RakNetProtocol.OnlinePing, new OnlinePing());
            MineNET_Registries.RakNetPacket.Add(RakNetProtocol.OfflinePing, new OfflinePing());
            MineNET_Registries.RakNetPacket.Add(RakNetProtocol.OnlinePong, new OnlinePong());

            MineNET_Registries.RakNetPacket.Add(RakNetProtocol.OpenConnectingRequest1, new OpenConnectingRequest1());
            MineNET_Registries.RakNetPacket.Add(RakNetProtocol.OpenConnectingReply1, new OpenConnectingReply1());
            MineNET_Registries.RakNetPacket.Add(RakNetProtocol.OpenConnectingRequest2, new OpenConnectingRequest2());
            MineNET_Registries.RakNetPacket.Add(RakNetProtocol.OpenConnectingReply2, new OpenConnectingReply2());
            MineNET_Registries.RakNetPacket.Add(RakNetProtocol.ClientConnectDataPacket, new ClientConnectDataPacket());
            MineNET_Registries.RakNetPacket.Add(RakNetProtocol.ServerHandShakeDataPacket, new ServerHandShakeDataPacket());
            MineNET_Registries.RakNetPacket.Add(RakNetProtocol.ClientHandShakeDataPacket, new ClientHandShakeDataPacket());

            MineNET_Registries.RakNetPacket.Add(RakNetProtocol.ClientDisconnectDataPacket, new ClientDisconnectDataPacket());

            MineNET_Registries.RakNetPacket.Add(RakNetProtocol.OfflinePong, new OfflinePong());

            MineNET_Registries.RakNetPacket.Add(RakNetProtocol.DataPacket0, new DataPacket0());
            MineNET_Registries.RakNetPacket.Add(RakNetProtocol.DataPacket1, new DataPacket1());
            MineNET_Registries.RakNetPacket.Add(RakNetProtocol.DataPacket2, new DataPacket2());
            MineNET_Registries.RakNetPacket.Add(RakNetProtocol.DataPacket3, new DataPacket3());
            MineNET_Registries.RakNetPacket.Add(RakNetProtocol.DataPacket4, new DataPacket4());
            MineNET_Registries.RakNetPacket.Add(RakNetProtocol.DataPacket5, new DataPacket5());
            MineNET_Registries.RakNetPacket.Add(RakNetProtocol.DataPacket6, new DataPacket6());
            MineNET_Registries.RakNetPacket.Add(RakNetProtocol.DataPacket7, new DataPacket7());
            MineNET_Registries.RakNetPacket.Add(RakNetProtocol.DataPacket8, new DataPacket8());
            MineNET_Registries.RakNetPacket.Add(RakNetProtocol.DataPacket9, new DataPacket9());
            MineNET_Registries.RakNetPacket.Add(RakNetProtocol.DataPacketA, new DataPacketA());
            MineNET_Registries.RakNetPacket.Add(RakNetProtocol.DataPacketB, new DataPacketB());
            MineNET_Registries.RakNetPacket.Add(RakNetProtocol.DataPacketC, new DataPacketC());
            MineNET_Registries.RakNetPacket.Add(RakNetProtocol.DataPacketD, new DataPacketD());
            MineNET_Registries.RakNetPacket.Add(RakNetProtocol.DataPacketE, new DataPacketE());
            MineNET_Registries.RakNetPacket.Add(RakNetProtocol.DataPacketF, new DataPacketF());

            MineNET_Registries.RakNetPacket.Add(RakNetProtocol.AckPacket, new Ack());
            MineNET_Registries.RakNetPacket.Add(RakNetProtocol.NackPacket, new Nack());

            MineNET_Registries.RakNetPacket.Add(RakNetProtocol.BatchPacket, new BatchPacket());
        }

        private void RegisterMinecraftPackets()
        {
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.LOGIN_PACKET, new LoginPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.PLAY_STATUS_PACKET, new PlayStatusPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.SERVER_TO_CLIENT_HANDSHAKE_PACKET, new ServerToClientHandshakePacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.CLIENT_TO_SERVER_HANDSHAKE_PACKET, new ClientToServerHandshakePacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.DISCONNECT_PACKET, new DisconnectPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.RESOURCE_PACKS_INFO_PACKET, new ResourcePacksInfoPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.RESOURCE_PACK_STACK_PACKET, new ResourcePackStackPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.RESOURCE_PACK_CLIENT_RESPONSE_PACKET, new ResourcePackClientResponsePacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.TEXT_PACKET, new TextPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.SET_TIME_PACKET, new SetTimePacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.START_GAME_PACKET, new StartGamePacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.ADD_PLAYER_PACKET, new AddPlayerPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.ADD_ENTITY_PACKET, new AddEntityPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.REMOVE_ENTITY_PACKET, new RemoveEntityPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.ADD_ITEM_ENTITY_PACKET, new AddItemEntityPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.ADD_HANGING_ENTITY_PACKET, new AddHangingEntityPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.TAKE_ITEM_ENTITY_PACKET, new TakeItemEntityPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.MOVE_ENTITY_ABSOLUTE_PACKET, new MoveEntityAbsolutePacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.MOVE_PLAYER_PACKET, new MovePlayerPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.RIDER_JUMP_PACKET, new RiderJumpPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.UPDATE_BLOCK_PACKET, new UpdateBlockPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.ADD_PAINTING_PACKET, new AddPaintingPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.EXPLODE_PACKET, new ExplodePacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.LEVEL_SOUND_EVENT_PACKET, new LevelSoundEventPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.LEVEL_EVENT_PACKET, new LevelEventPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.BLOCK_EVENT_PACKET, new BlockEventPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.ENTITY_EVENT_PACKET, new EntityEventPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.MOB_EFFECT_PACKET, new MobEffectPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.UPDATE_ATTRIBUTES_PACKET, new UpdateAttributesPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.INVENTORY_TRANSACTION_PACKET, new InventoryTransactionPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.MOB_EQUIPMENT_PACKET, new MobEquipmentPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.MOB_ARMOR_EQUIPMENT_PACKET, new MobArmorEquipmentPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.INTERACT_PACKET, new InteractPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.BLOCK_PICK_REQUEST_PACKET, new BlockPickRequestPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.ENTITY_PICK_REQUEST_PACKET, new EntityPickRequestPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.PLAYER_ACTION_PACKET, new PlayerActionPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.ENTITY_FALL_PACKET, new EntityFallPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.HURT_ARMOR_PACKET, new HurtArmorPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.SET_ENTITY_DATA_PACKET, new SetEntityDataPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.SET_ENTITY_MOTION_PACKET, new SetEntityMotionPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.SET_ENTITY_LINK_PACKET, new SetEntityLinkPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.SET_HEALTH_PACKET, new SetHealthPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.SET_SPAWN_POSITION_PACKET, new SetSpawnPositionPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.ANIMATE_PACKET, new AnimatePacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.RESPAWN_PACKET, new RespawnPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.CONTAINER_OPEN_PACKET, new ContainerOpenPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.CONTAINER_CLOSE_PACKET, new ContainerClosePacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.PLAYER_HOTBAR_PACKET, new PlayerHotbarPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.INVENTORY_CONTENT_PACKET, new InventoryContentPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.INVENTORY_SLOT_PACKET, new InventorySlotPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.CONTAINER_SET_DATA_PACKET, new ContainerSetDataPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.CRAFTING_DATA_PACKET, new CraftingDataPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.CRAFTING_EVENT_PACKET, new CraftingEventPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.GUI_DATA_PICK_ITEM_PACKET, new GuiDataPickItemPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.ADVENTURE_SETTINGS_PACKET, new AdventureSettingsPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.BLOCK_ENTITY_DATA_PACKET, new BlockEntityDataPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.PLAYER_INPUT_PACKET, new PlayerInputPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.FULL_CHUNK_DATA_PACKET, new FullChunkDataPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.SET_COMMANDS_ENABLED_PACKET, new SetCommandsEnabledPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.SET_DIFFICULTY_PACKET, new SetDifficultyPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.CHANGE_DIMENSION_PACKET, new ChangeDimensionPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.SET_PLAYER_GAME_TYPE_PACKET, new SetPlayerGameTypePacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.PLAYER_LIST_PACKET, new PlayerListPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.SIMPLE_EVENT_PACKET, new SimpleEventPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.EVENT_PACKET, new EventPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.SPAWN_EXPERIENCE_ORB_PACKET, new SpawnExperienceOrbPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.CLIENTBOUND_MAP_ITEM_DATA_PACKET, new ClientboundMapItemDataPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.MAP_INFO_REQUEST_PACKET, new MapInfoRequestPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.REQUEST_CHUNK_RADIUS_PACKET, new RequestChunkRadiusPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.CHUNK_RADIUS_UPDATED_PACKET, new ChunkRadiusUpdatedPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.ITEM_FRAME_DROP_ITEM_PACKET, new ItemFrameDropItemPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.GAME_RULES_CHANGED_PACKET, new GameRulesChangedPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.CAMERA_PACKET, new CameraPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.BOSS_EVENT_PACKET, new BossEventPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.SHOW_CREDITS_PACKET, new ShowCreditsPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.AVAILABLE_COMMANDS_PACKET, new AvailableCommandsPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.COMMAND_REQUEST_PACKET, new CommandRequestPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.COMMAND_BLOCK_UPDATE_PACKET, new CommandBlockUpdatePacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.COMMAND_OUTPUT_PACKET, new CommandOutputPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.UPDATE_TRADE_PACKET, new UpdateTradePacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.UPDATE_EQUIPMENT_PACKET, new UpdateEquipPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.RESOURCE_PACK_DATA_INFO_PACKET, new ResourcePackDataInfoPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.RESOURCE_PACK_CHUNK_DATA_PACKET, new ResourcePackChunkDataPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.RESOURCE_PACK_CHUNK_REQUEST_PACKET, new ResourcePackChunkRequestPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.TRANSFER_PACKET, new TransferPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.PLAY_SOUND_PACKET, new PlaySoundPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.STOP_SOUND_PACKET, new StopSoundPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.SET_TITLE_PACKET, new SetTitlePacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.ADD_BEHAVIOR_TREE_PACKET, new AddBehaviorTreePacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.STRUCTURE_BLOCK_UPDATE_PACKET, new StructureBlockUpdatePacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.SHOW_STORE_OFFER_PACKET, new ShowStoreOfferPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.PURCHASE_RECEIPT_PACKET, new PurchaseReceiptPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.PLAYER_SKIN_PACKET, new PlayerSkinPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.SUB_CLIENT_LOGIN_PACKET, new SubClientLoginPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.W_S_CONNECT_PACKET, new WSConnectPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.SET_LAST_HURT_BY_PACKET, new SetLastHurtByPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.BOOK_EDIT_PACKET, new BookEditPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.NPC_REQUEST_PACKET, new NpcRequestPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.PHOTO_TRANSFER_PACKET, new PhotoTransferPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.MODAL_FORM_REQUEST_PACKET, new ModalFormRequestPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.MODAL_FORM_RESPONSE_PACKET, new ModalFormResponsePacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.SERVER_SETTINGS_REQUEST_PACKET, new ServerSettingsRequestPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.SERVER_SETTINGS_RESPONSE_PACKET, new ServerSettingsResponsePacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.SHOW_PROFILE_PACKET, new ShowProfilePacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.SET_DEFAULT_GAME_TYPE_PACKET, new SetDefaultGameTypePacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.REMOVE_OBJECTIVE_PACKET, new RemoveObjectivePacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.SET_DISPLAY_OBJECTIVE_PACKET, new SetDisplayObjectivePacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.SET_SCORE_PACKET, new SetScorePacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.LAB_TABLE_PACKET, new LabTablePacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.UPDATE_BLOCK_SYNCED_PACKET, new UpdateBlockSyncedPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.MOVE_ENTITY_DELTA_PACKET, new MoveEntityDeltaPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.SET_LOCAL_PLAYER_AS_INITIALIZED_PACKET, new SetLocalPlayerAsInitializedPacket());
        }
        #endregion

        #region Thread Method
        private void ReceiveClock()
        {
            while (this.IsRunNetwork)
            {
                this.ReceiveUpdate();
            }
        }

        private void ReceiveUpdate()
        {
            try
            {
                if (this.IsRunNetwork)
                {
                    IPEndPoint point = null;
                    byte[] bytes = this.Client.Receive(ref point);

                    this.HandleRakNetPacket(point, bytes);
                }
            }
            catch (ThreadAbortException e1)
            {
                OutLog.Log("%server.network.packetThreadAbort", e1.GetType().Name);
            }
            catch (Exception e2)
            {
                OutLog.Notice(e2);
            }
        }

        public void OnUpdate()
        {
            while (this.IsRunNetwork)
            {
                Server.Instance.Clock.Start("network.update");
                foreach (KeyValuePair<string, NetworkSession> session in this.Sessions)
                {
                    session.Value.OnUpdate();
                }
                Server.Instance.Clock.Stop("network.update");
            }
        }
        #endregion

        #region Packet Handle Method
        private void HandleRakNetPacket(IPEndPoint endPoint, byte[] bytes)
        {
            if (bytes?.Length != 0)
            {
                byte msgId = bytes[0];
                RakNetPacket pk = this.GetRakNetPacket(msgId, bytes);
                if (pk != null)
                {
                    RakNetPacketReceiveEventArgs ev = new RakNetPacketReceiveEventArgs(endPoint, pk);
                    Server.Instance.Event.Network.OnRakNetPacketReceive(this, ev);

                    if (ev.IsCancel)
                    {
                        return;
                    }
                    pk = ev.Packet;

                    if (pk is OfflineMessage)
                    {
                        if (this.SessionCreated(endPoint))
                        {
                            OutLog.Log("%server.network.raknet.sessionCreated", endPoint);
                            return;
                        }

                        this.HandleOfflineMessage(endPoint, (OfflineMessage) pk);
                        pk.Dispose();
                        return;
                    }

                    if (!this.SessionCreated(endPoint))
                    {
                        OutLog.Log("%server.network.raknet.sessionNotCreated", endPoint);
                        return;
                    }

                    if (pk is DataPacket)
                    {
                        NetworkSession session = this.GetSession(endPoint);
                        session.HandleDataPacket((DataPacket) pk);
                        pk.Dispose();
                        return;
                    }

                    if (pk is AcknowledgePacket)
                    {
                        NetworkSession session = this.GetSession(endPoint);
                        session.HandleAcknowledgePacket((AcknowledgePacket) pk);
                        pk.Dispose();
                        return;
                    }
                }

                OutLog.Log("%server.network.raknet.notHandle", msgId.ToString("X"));
            }
        }

        private void HandleOfflineMessage(IPEndPoint endPoint, OfflineMessage msg)
        {
            if (msg.MessageID == RakNetProtocol.OfflinePing)
            {
                OfflinePing ping = (OfflinePing) msg;
                OfflinePong pong = (OfflinePong) this.GetRakNetPacket(RakNetProtocol.OfflinePong);
                pong.Ping = ping.Ping;
                pong.ServerID = this.ServerID;
                pong.ServerName = Server.Instance.ServerList.ToString();

                this.Send(endPoint, pong);
            }
            else if (msg.MessageID == RakNetProtocol.OpenConnectingRequest1)
            {
                OpenConnectingRequest1 req1 = (OpenConnectingRequest1) msg;
                OpenConnectingReply1 rep1 = (OpenConnectingReply1) this.GetRakNetPacket(RakNetProtocol.OpenConnectingReply1);
                rep1.ServerID = this.ServerID;
                rep1.MTUSize = req1.MTUSize;

                this.Send(endPoint, rep1);
            }
            else if (msg.MessageID == RakNetProtocol.OpenConnectingRequest2)
            {
                OpenConnectingRequest2 req2 = (OpenConnectingRequest2) msg;
                OpenConnectingReply2 rep2 = (OpenConnectingReply2) this.GetRakNetPacket(RakNetProtocol.OpenConnectingReply2);
                rep2.ServerID = this.ServerID;
                rep2.EndPoint = req2.EndPoint;
                rep2.MTUSize = req2.MTUSize;

                if (req2.EndPoint.Port == Server.Instance.EndPoint.Port)
                {
                    this.SessionCreate(endPoint, req2.ClientID, req2.MTUSize);
                }

                this.Send(endPoint, rep2);
            }
        }
        #endregion

        #region Session Method
        public void SessionCreate(IPEndPoint endPoint, long clientID, short mtuSize)
        {
            if (!this.SessionCreated(endPoint))
            {
                CreateSessionEventArgs ev = new CreateSessionEventArgs(endPoint, new NetworkSession(endPoint, clientID, mtuSize));
                Server.Instance.Event.Network.OnCreateSession(this, ev);

                if (ev.IsCancel)
                {
                    return;
                }

                this.Sessions.TryAdd(endPoint.ToString(), ev.Session);
                this.CreatePlayer(endPoint);
                OutLog.Info("%server.network.raknet.sessionCreate", endPoint, mtuSize);
            }
        }

        public bool SessionCreated(IPEndPoint endPoint)
        {
            if (this.Sessions.ContainsKey(endPoint.ToString()))
            {
                return true;
            }

            return false;
        }

        public NetworkSession GetSession(IPEndPoint endPoint)
        {
            string endPointStr = endPoint.ToString();
            if (this.Sessions.ContainsKey(endPointStr))
            {
                return this.Sessions[endPointStr];
            }

            return null;
        }

        public void RemoveSession(IPEndPoint endPoint)
        {
            string endPointStr = endPoint.ToString();
            if (this.Sessions.ContainsKey(endPointStr))
            {
                NetworkSession session;
                this.RemovePlayer(endPoint);
                this.Sessions[endPointStr].Close();
                this.Sessions.TryRemove(endPointStr, out session);
            }
        }
        #endregion

        #region Player Method
        public void CreatePlayer(IPEndPoint endPoint)
        {
            string endPointStr = endPoint.ToString();
            if (!this.Players.ContainsKey(endPointStr))
            {
                PlayerCreateEventArgs ev = new PlayerCreateEventArgs(new Player());
                Server.Instance.Event.Player.OnPlayerCreate(this, ev);

                ev.CustomPlayer.EndPoint = endPoint;

                this.Players.TryAdd(endPointStr, ev.CustomPlayer);
            }
        }

        public void RemovePlayer(IPEndPoint endPoint)
        {
            string endPointStr = endPoint.ToString();
            if (this.Players.ContainsKey(endPointStr))
            {
                Player player;
                this.Players.TryRemove(endPointStr, out player);
            }
        }
        #endregion

        #region Get Registry Packet Method
        public RakNetPacket GetRakNetPacket(int msgId, byte[] buffer = null)
        {
            RakNetPacket pk = null;
            MineNET_Registries.RakNetPacket.TryGetValue(msgId, out pk);

            if (pk != null && buffer != null)
            {
                pk.SetBuffer(buffer);
                pk.Decode();
            }

            return pk;
        }

        public MinecraftPacket GetMinecraftPacket(int msgId, byte[] buffer = null)
        {
            MinecraftPacket pk = null;
            MineNET_Registries.MinecraftPacket.TryGetValue(msgId, out pk);

            if (pk != null && buffer != null)
            {
                pk.SetBuffer(buffer);
                pk.Decode();
            }

            return pk;
        }
        #endregion

        #region Send Message Method
        public void Send(IPEndPoint point, RakNetPacket msg)
        {
            msg.Encode();

            RakNetPacketSendEventArgs ev = new RakNetPacketSendEventArgs(point, msg);
            Server.Instance.Event.Network.OnRakNetPacketSend(this, ev);

            if (ev.IsCancel)
            {
                return;
            }
            msg = ev.Packet;

            byte[] buffer = msg.ToArray();
            this.Client.Send(buffer, buffer.Length, point);
        }
        #endregion

        #region Dispose Method
        public void Dispose()
        {
            this.IsRunNetwork = false;
            this.ReceiveThread.Abort();
            this.ReceiveThread = null;
        }
        #endregion
    }
}
