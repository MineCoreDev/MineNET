using MineNET.Entities.Players;
using MineNET.Events.NetworkEvents;
using MineNET.Events.PlayerEvents;
using MineNET.Network.MinecraftPackets;
using MineNET.Network.RakNetPackets;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

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
            client.DontFragment = false;
            client.EnableBroadcast = false;
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
            //3
            //4
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.DISCONNECT_PACKET, new DisconnectPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.RESOURCE_PACKS_INFO_PACKET, new ResourcePacksInfoPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.RESOURCE_PACK_STACK_PACKET, new ResourcePackStackPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.RESOURCE_PACK_CLIENT_RESPONSE_PACKET, new ResourcePackClientResponsePacket());
            //9
            //A
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.START_GAME_PACKET, new StartGamePacket());
            //C
            //D
            //E
            //F
            //10
            //11
            //12
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.MOVE_PLAYER_PACKET, new MovePlayerPacket());
            //14
            //15
            //16
            //17
            //18
            //19
            //1A
            //1B
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.MOB_EFFECT_PACKET, new MobEffectPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.UPDATE_ATTRIBUTES_PACKET, new UpdateAttributesPacket());
            //1E
            //1F
            //20
            //21
            //22
            //23
            //24
            //25
            //26
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.SET_ENTITY_DATA_PACKET, new SetEntityDataPacket());
            //28
            //29
            //2A
            //2B
            //2C
            //2D
            //2E
            //2F
            //30
            //31
            //32
            //33
            //34
            //35
            //36
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.ADVENTURE_SETTINGS_PACKET, new AdventureSettingsPacket());
            //38
            //39
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.FULL_CHUNK_DATA_PACKET, new FullChunkDataPacket());
            //3B
            //3C
            //3D
            //3E
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.PLAYER_LIST_PACKET, new PlayerListPacket());
            //40
            //41
            //42
            //43
            //44
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.REQUEST_CHUNK_RADIUS_PACKET, new RequestChunkRadiusPacket());
            MineNET_Registries.MinecraftPacket.Add(MinecraftProtocol.CHUNK_RADIUS_UPDATED_PACKET, new ChunkRadiusUpdatedPacket());
            //47
            //48
            //49
            //4A
            //4B
            //4C
            //4D
            //4E
            //4F
            //50
            //51
            //52
            //53
            //54
            //55
            //56
            //57
            //58
            //59
            //5A
            //5B
            //5C
            //5D
            //5E
            //5F
            //60
            //61
            //62
            //63
            //64
            //65
            //66
            //67
            //68
            //69
            //6A
            //6B
            //6C
            //6D
            //6E
        }
        #endregion

        #region Thread Method
        private void ReceiveClock()
        {
            while (IsRunNetwork)
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
            catch (Exception e)
            {
                OutLog.Notice(e);
            }
        }

        public void OnUpdate()
        {
            while (IsRunNetwork)
            {
                foreach (KeyValuePair<string, NetworkSession> session in this.Sessions)
                {
                    session.Value.OnUpdate();
                }
                Thread.Sleep(1);
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
