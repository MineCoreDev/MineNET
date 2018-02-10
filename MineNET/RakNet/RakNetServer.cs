using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using MineNET.RakNet.Packets;
using MineNET.Utils;

namespace MineNET.RakNet
{
    public class RakNetServer
    {
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

        Timer serverUpdater;

        Dictionary<byte, Packet> packetPool = new Dictionary<byte, Packet>();

        long serverID;
        ushort port;

        Dictionary<string, int> blockUsers = new Dictionary<string, int>();
        Dictionary<string, RakNetSession> sessions = new Dictionary<string, RakNetSession>();

        public RakNetServer(ushort port)
        {
            Logger.Info(LangManager.GetString("network_start"));
            Logger.Info(LangManager.GetString("network_start_port"), port);

            this.UDPClientInit(port);
            this.Init();

            this.port = port;

            Logger.Info(LangManager.GetString("network_started"));
        }

        ~RakNetServer()
        {
            this.client.Close();
            this.serverUpdater.Dispose();
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
            this.client.Client.IOControl((int)SIO_UDP_CONNRESET, new byte[] { Convert.ToByte(false) }, null);

            this.client.BeginReceive(OnReceive, null);
        }

        private void Init()
        {
            this.serverUpdater = new Timer(OnUpdate, null, 0, 1000 / 20);

            this.serverID = new Random().Next(int.MinValue, int.MaxValue);

            this.RegisterPackets();
        }

        public long GetServerID()
        {
            return this.serverID;
        }

        public int GetPort()
        {
            return this.port;
        }

        private void OnUpdate(object state)
        {
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
        }

        private void OnReceive(IAsyncResult result)
        {
            IPEndPoint point = null;
            byte[] buffer = null;
            try
            {
                buffer = this.client.EndReceive(result, ref point);

                this.client.BeginReceive(OnReceive, null);

                if (this.blockUsers.ContainsKey(IPEndPointToID(point)))
                {
                    return;
                }

                if (buffer.Length != 0)
                {
                    this.HandlePacket(point, buffer);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                this.BlockUser(point, 20 * 5);
            }
        }

        private void HandlePacket(IPEndPoint point, byte[] buffer)
        {
            byte pid = buffer[0];
            Packet pk = GetPacketPool(pid, buffer);
            string id = IPEndPointToID(point);

            if (pk is DataPacket)
            {
                if (sessions.ContainsKey(id))
                {
                    sessions[id].DataPacketHandle(pk);
                }
                else
                {
                    Logger.Log(LangManager.GetString("raknet_sessionNotCreate"), IPEndPointToID(point));
                }
            }
            else if (pk is OfflineMessage)
            {
                if (sessions.ContainsKey(id))
                {
                    Logger.Log(LangManager.GetString("raknet_sessionCreated"), IPEndPointToID(point));
                }
                else
                {
                    this.OfflineMessageHandler(pk, point);
                }
            }
            else
            {
                Logger.Log("NotHandlePacket: {0}", pid);
                return;
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

            packet.Encode();

            byte[] bytes = packet.GetResult();
            this.client.BeginSend(bytes, bytes.Length, point, OnSend, null);
        }

        private void OfflineMessageHandler(Packet packet, IPEndPoint point)
        {
            packet.Decode();
            if (packet.PacketID == UNCONNECTED_PING.ID)
            {
                UNCONNECTED_PING uping = (UNCONNECTED_PING)packet;
                UNCONNECTED_PONG upong = new UNCONNECTED_PONG();
                upong.PingID = uping.PingID;
                upong.ServerID = GetServerID();
                upong.ServerName = "MCPE;MineNETServer;160;1.2.8;0;20;MineNET;Survival";

                this.SendPacket(upong, point.Address, point.Port);
            }
            else if (packet.PacketID == OPEN_CONNECTION_REQUEST_1.ID)
            {
                OPEN_CONNECTION_REQUEST_1 request1 = (OPEN_CONNECTION_REQUEST_1)packet;
                OPEN_CONNECTION_REPLY_1 reply1 = new OPEN_CONNECTION_REPLY_1();
                reply1.ServerID = GetServerID();
                reply1.MTUSize = request1.MTUSize;

                this.SendPacket(reply1, point.Address, point.Port);
            }
            else if (packet.PacketID == OPEN_CONNECTION_REQUEST_2.ID)
            {
                OPEN_CONNECTION_REQUEST_2 request2 = (OPEN_CONNECTION_REQUEST_2)packet;
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

            if (this.sessions.Count > 4096)
            {
                //TODO: SendMessage...
                return;
            }

            RakNetSession session = new RakNetSession(this, point, clientID, mtuSize);
            this.sessions.Add(id, session);

            MineNETServer.Instance.NetworkManager.CreatePlayer(point, IPEndPointToID(point));

            Logger.Info(LangManager.GetString("raknet_sessionCreate"), IPEndPointToID(point), mtuSize);
        }

        public void RemoveSession(IPEndPoint point, string msg)
        {
            string id = IPEndPointToID(point);

            Logger.Info(LangManager.GetString("raknet_sessionClose"), IPEndPointToID(point));
            Logger.Log(LangManager.GetString("raknet_sessionClose_reason"), msg);

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
            Logger.Warning(LangManager.GetString("raknet_userBlock"), IPEndPointToID(point), blockTime / 20);
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
                pk = (Packet)pk.Clone();
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
