using System.Net;
using MineNET.RakNet.Packets;
using MineNET.Utils;

namespace MineNET.RakNet
{
    public sealed class RakNetSession
    {
        const int STATE_CONNECTING = 0;
        const int STATE_CONNECTED = 1;

        RakNetServer server;

        IPEndPoint point;
        public IPEndPoint EndPoint
        {
            get
            {
                return point;
            }
        }
        long clientID;
        public long ClientID
        {
            get
            {
                return clientID;
            }
        }
        short mtuSize;
        public long MTUSize
        {
            get
            {
                return mtuSize;
            }
        }

        int state = STATE_CONNECTING;
        int sendSeqNumber = 0;

        int timedOut;

        public RakNetSession(RakNetServer server, IPEndPoint point, long clientID, short mtuSize)
        {
            this.server = server;
            this.point = point;
            this.clientID = clientID;
            this.mtuSize = mtuSize;
            this.timedOut = 100;
        }

        internal void DataPacketHandle(Packet pk)
        {
            if (pk is DataPacket)
            {
                DataPacket packet = (DataPacket)pk;
                packet.Decode();

                this.timedOut = 100;

                for (int i = 0; i < packet.Packets.Length; ++i)
                {
                    if (packet.Packets[i] is EncapsulatedPacket)
                    {
                        this.EncapsulatedPacketHandle((EncapsulatedPacket)packet.Packets[i]);
                    }
                }
            }
            else
            {
                if (pk is ACK)
                {
                    Logger.Log("Handle ACK");
                }
                else if (pk is NACK)
                {
                    Logger.Log("Handle NACK");
                }
            }
        }

        private void EncapsulatedPacketHandle(EncapsulatedPacket packet)
        {
            int id = packet.buffer[0];
            if (id < 0x80)
            {
                if (id == CLIENT_DISCONNECT_DataPacket.ID)
                {
                    Close("ClientDisconnect");
                }
                else if (state == STATE_CONNECTING)
                {
                    EncapsulatedPacketHandler(packet, id);
                }
                else if (id == PING_DataPacket.ID)
                {
                    PING_DataPacket ping = new PING_DataPacket();
                    ping.SetBuffer(packet.buffer);
                    ping.Decode();

                    PONG_DataPacket pong = new PONG_DataPacket();
                    pong.PingID = ping.PingID;
                    pong.Encode();

                    EncapsulatedPacket enc = new EncapsulatedPacket();
                    enc.buffer = pong.GetResult();
                    enc.reliability = PacketReliability.UNRELIABLE;

                    SendPacket(enc);
                }
            }
            else if (id == 0xfe && state == STATE_CONNECTED)
            {
                MineNETServer.Instance.NetworkManager.HandleBatchPacket(this, packet.buffer);
            }
        }

        private void EncapsulatedPacketHandler(EncapsulatedPacket packet, int id)
        {
            if (id == CLIENT_CONNECT_DataPacket.ID)
            {
                CLIENT_CONNECT_DataPacket ccd = new CLIENT_CONNECT_DataPacket();
                ccd.SetBuffer(packet.buffer);
                ccd.Decode();

                SERVER_HANDSHAKE_DataPacket shd = new SERVER_HANDSHAKE_DataPacket();
                shd.EndPoint = point;
                shd.SendPing = ccd.SendPing;
                shd.SendPong = ccd.SendPing + 1000;
                shd.Encode();

                EncapsulatedPacket enc = new EncapsulatedPacket();
                enc.buffer = shd.GetResult();
                enc.reliability = PacketReliability.UNRELIABLE;

                SendPacket(enc);
            }
            else if (id == CLIENT_HANDSHAKE_DataPacket.ID)
            {
                CLIENT_HANDSHAKE_DataPacket chd = new CLIENT_HANDSHAKE_DataPacket();
                chd.SetBuffer(packet.buffer);
                chd.Decode();

                if (chd.EndPoint.Port == server.GetPort())
                {
                    state = STATE_CONNECTED;
                }
            }
        }

        public void SendPacket(EncapsulatedPacket packet)
        {
            DataPacket_0 pk = new DataPacket_0();
            pk.SeqNumber = sendSeqNumber++;
            pk.Packets = new[]
            {
                packet
            };

            server.SendPacket(pk, point.Address, point.Port);
        }

        internal void Update()
        {
            if (this.timedOut <= 0)
            {
                this.Close("timedout");
            }
            this.timedOut--;
        }

        internal void Close(string msg)
        {
            MineNETServer.Instance.NetworkManager.RemovePlayer(RakNetServer.IPEndPointToID(point));
            this.server.RemoveSession(this.point, msg);
        }
    }
}
