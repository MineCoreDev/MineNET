using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using MineNET.RakNet.Packets;
using MineNET.Utils;

namespace MineNET.RakNet
{
    public class RakNetServer
    {
        public const int UPDATE_TICK = 200;

        sealed class ReceiveData
        {
            public IPEndPoint Point { get; set; }
            public byte[] Data { get; set; }
        }

        public static readonly byte[] Magic = new byte[]
        {
            0x00,
            0xff,
            0xff,
            0x00,
            0xfe,
            0xfe,
            0xfe,
            0xfe,
            0xfd,
            0xfd,
            0xfd,
            0xfd,
            0x12,
            0x34,
            0x56,
            0x78
        };

        UdpClient client;

        Dictionary<byte, Packet> packetPool = new Dictionary<byte, Packet>();

        long serverID;
        ushort port;

        Dictionary<string, int> blockUsers = new Dictionary<string, int>();
        Dictionary<string, RakNetSession> sessions = new Dictionary<string, RakNetSession>();

        Queue<ReceiveData> receiveDataQueue = new Queue<ReceiveData>();

        public RakNetServer(ushort port)
        {
            Logger.Info("%network_start");

            this.UDPClientInit(port);
            this.Init();

            Logger.Info("%network_start_port", port);

            this.port = port;

            Logger.Info("%network_started");
        }

        ~RakNetServer()
        {
            this.client.Close();
        }

        private void UDPClientInit(ushort port)
        {
            this.client = new UdpClient(new IPEndPoint(IPAddress.Any, port));
            this.client.Client.ReceiveBufferSize = int.MaxValue;
            this.client.Client.SendBufferSize = int.MaxValue;
            this.client.DontFragment = false;
            this.client.EnableBroadcast = false;
            uint IOC_IN = 0x80000000;
            uint IOC_VENDOR = 0x18000000;
            uint SIO_UDP_CONNRESET = IOC_IN | IOC_VENDOR | 12;
            this.client.Client.IOControl((int) SIO_UDP_CONNRESET, new byte[] { Convert.ToByte(false) }, null);

            this.client.BeginReceive(OnReceive, null);
        }

        private void Init()
        {
            this.serverID = new Random().Next(int.MinValue, int.MaxValue);

            this.RegisterPackets();

            OnUpdate();
        }

        public long GetServerID()
        {
            return this.serverID;
        }

        public int GetPort()
        {
            return this.port;
        }

        private async void OnUpdate()
        {
            while (!Server.Instance.IsShutdown())
            {
                if (receiveDataQueue.Count > 0)
                {
                    ReceiveData data = receiveDataQueue.Dequeue();
                    this.HandlePacket(data.Point, data.Data);
                }

                string[] bl = this.blockUsers.Keys.ToArray();
                for (int i = 0; i < bl.Length; ++i)
                {
                    this.blockUsers[bl[i]] -= 1;
                    if (this.blockUsers[bl[i]] <= 0)
                    {
                        this.blockUsers.Remove(bl[i]);
                    }
                }

                RakNetSession[] sl = this.sessions.Values.ToArray();
                for (int i = 0; i < sl.Length; ++i)
                {
                    sl[i].Update();
                }

                await Task.Delay(1000 / UPDATE_TICK);
            }
        }

        private void OnReceive(IAsyncResult result)
        {
            IPEndPoint point = null;
            byte[] buffer = null;
            try
            {
                buffer = this.client.EndReceive(result, ref point);

                if (this.blockUsers.ContainsKey(IPEndPointToID(point)))
                {
                    return;
                }

                if (buffer.Length != 0)
                {
                    ReceiveData data = new ReceiveData()
                    {
                        Point = point,
                        Data = buffer
                    };

                    this.receiveDataQueue.Enqueue(data);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                this.BlockUser(point, UPDATE_TICK * 5);
            }
            finally
            {
                this.client.BeginReceive(OnReceive, null);
            }
        }

        private void HandlePacket(IPEndPoint point, byte[] buffer)
        {
            byte pid = buffer[0];
            using (Packet pk = GetPacketPool(pid, buffer))
            {
                string id = IPEndPointToID(point);

                if (pk is DataPacket || pk is ACK || pk is NACK)
                {
                    if (sessions.ContainsKey(id))
                    {
                        sessions[id].DataPacketHandle(pk);
                    }
                    else
                    {
                        Logger.Log("%raknet_sessionNotCreate", IPEndPointToID(point));
                    }
                }
                else if (pk is OfflineMessage)
                {
                    if (sessions.ContainsKey(id))
                    {
                        Logger.Log("%raknet_sessionCreated", IPEndPointToID(point));
                    }
                    else
                    {
                        this.OfflineMessageHandler(pk, point);
                    }
                }
                else
                {
                    //Logger.Log("NotHandlePacket: {0}", pid);
                    return;
                }
            }
        }

        private void OnSend(IAsyncResult result)
        {
            try
            {
                int length = this.client.EndSend(result);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        public void SendPacket(Packet packet, IPAddress ip, int port)
        {
            IPEndPoint point = new IPEndPoint(ip, port);

            if (packet == null)
                return;

            packet.Encode();

            byte[] bytes = packet.GetResult();
            this.client.BeginSend(bytes, bytes.Length, point, OnSend, null);

            packet.Dispose();
        }

        private void OfflineMessageHandler(Packet packet, IPEndPoint point)
        {
            packet.Decode();
            if (packet.PacketID == UNCONNECTED_PING.ID)
            {
                string motd = Server.ServerConfig.ServerMotd;
                int protocol = Network.Packets.ProtocolInfo.CLIENT_PROTOCOL;
                string version = Network.Packets.ProtocolInfo.CLIENT_VERSION;
                int maxPlayer = Server.ServerConfig.MaxPlayers;
                int player = Server.Instance.NetworkManager.players.Count;
                string gameMode = Server.ServerConfig.GameMode;

                UNCONNECTED_PING uping = (UNCONNECTED_PING) packet;
                UNCONNECTED_PONG upong = new UNCONNECTED_PONG();
                upong.PingID = uping.PingID;
                upong.ServerID = GetServerID();
                upong.ServerName = $"MCPE;{motd};{protocol};{version};{player};{maxPlayer};MineNET;{gameMode}";

                this.SendPacket(upong, point.Address, point.Port);
            }
            else if (packet.PacketID == OPEN_CONNECTION_REQUEST_1.ID)
            {
                OPEN_CONNECTION_REQUEST_1 request1 = (OPEN_CONNECTION_REQUEST_1) packet;
                OPEN_CONNECTION_REPLY_1 reply1 = new OPEN_CONNECTION_REPLY_1();
                reply1.ServerID = GetServerID();
                reply1.MTUSize = request1.MTUSize;

                this.SendPacket(reply1, point.Address, point.Port);
            }
            else if (packet.PacketID == OPEN_CONNECTION_REQUEST_2.ID)
            {
                OPEN_CONNECTION_REQUEST_2 request2 = (OPEN_CONNECTION_REQUEST_2) packet;
                OPEN_CONNECTION_REPLY_2 reply2 = new OPEN_CONNECTION_REPLY_2();
                reply2.ServerID = GetServerID();
                reply2.EndPoint = request2.EndPoint;
                reply2.MTUSize = request2.MTUSize;

                if (this.GetSession(request2.EndPoint) == null && request2.EndPoint.Port == this.GetPort())
                {
                    this.CreateSession(point, request2.ClientID, request2.MTUSize);
                }

                this.SendPacket(reply2, point.Address, point.Port);
            }
        }

        public RakNetSession GetSession(IPEndPoint point)
        {
            string id = IPEndPointToID(point);
            if (sessions.ContainsKey(id))
            {
                return sessions[id];
            }
            return null;
        }

        public void CreateSession(IPEndPoint point, long clientID, short mtuSize)
        {
            string id = IPEndPointToID(point);

            if (this.sessions.ContainsKey(id))
            {
                return;
            }

            if (this.sessions.Count > 4096)
            {
                //TODO: SendMessage...
                return;
            }

            RakNetSession session = new RakNetSession(this, point, clientID, mtuSize);
            this.sessions.Add(id, session);

            Server.Instance.NetworkManager.CreatePlayer(point, IPEndPointToID(point));

            Logger.Info("%raknet_sessionCreate", IPEndPointToID(point), mtuSize);
        }

        public void RemoveSession(IPEndPoint point, string msg)
        {
            string id = IPEndPointToID(point);

            Logger.Info("%raknet_sessionClose", IPEndPointToID(point));
            Logger.Log("%raknet_sessionClose_reason", msg);

            this.sessions.Remove(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <param name="blockTime">Ticks</param>
        public void BlockUser(IPEndPoint point, int blockTime)
        {
            this.blockUsers.Add(IPEndPointToID(point), blockTime);
            Logger.Warning("%raknet_userBlock", IPEndPointToID(point), blockTime / UPDATE_TICK);
        }

        public void UnBlockUser(IPEndPoint point)
        {
            this.blockUsers.Remove(IPEndPointToID(point));
        }

        private void RegisterPacket(byte id, Packet packet)
        {
            this.packetPool.Add(id, packet);
        }

        public Packet GetPacketPool(byte id, byte[] buffer)
        {
            if (this.packetPool.ContainsKey(id))
            {
                Packet pk = this.packetPool[id];
                pk = (Packet) pk.Clone();
                pk.SetBuffer(buffer);
                return pk;
            }
            else
            {
                return null;
            }
        }

        public static string IPEndPointToID(IPEndPoint point)
        {
            return point.Address.ToString() + ":" + point.Port;
        }

        private void RegisterPackets()
        {
            this.RegisterPacket(UNCONNECTED_PING.ID, new UNCONNECTED_PING());
            this.RegisterPacket(UNCONNECTED_PONG.ID, new UNCONNECTED_PONG());

            this.RegisterPacket(OPEN_CONNECTION_REQUEST_1.ID, new OPEN_CONNECTION_REQUEST_1());
            this.RegisterPacket(OPEN_CONNECTION_REPLY_1.ID, new OPEN_CONNECTION_REPLY_1());
            this.RegisterPacket(OPEN_CONNECTION_REQUEST_2.ID, new OPEN_CONNECTION_REQUEST_2());
            this.RegisterPacket(OPEN_CONNECTION_REPLY_2.ID, new OPEN_CONNECTION_REPLY_2());

            this.RegisterPacket(DataPacket_0.ID, new DataPacket_0());
            this.RegisterPacket(DataPacket_4.ID, new DataPacket_4());
            this.RegisterPacket(DataPacket_C.ID, new DataPacket_C());

            this.RegisterPacket(ACK.ID, new ACK());
            this.RegisterPacket(NACK.ID, new NACK());
        }
    }
}
