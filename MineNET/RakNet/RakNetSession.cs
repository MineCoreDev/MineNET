using System.Collections.Generic;
using System.Linq;
using System.Net;
using MineNET.RakNet.Packets;
using MineNET.Utils;

//TODO: Ack Nack Bug
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
                return this.point;
            }
        }

        long clientID;
        public long ClientID
        {
            get
            {
                return this.clientID;
            }
        }

        short mtuSize;
        public long MTUSize
        {
            get
            {
                return this.mtuSize;
            }
        }

        int state = STATE_CONNECTING;
        int sendSeqNumber = 0;
        int splitID = 0;
        int startSeq = -1;
        int endSeq = 2048;
        int lastSeqNumber = -1;

        Dictionary<int, EncapsulatedPacket> reliableWindow = new Dictionary<int, EncapsulatedPacket>();
        Dictionary<int, int> receivedWindow = new Dictionary<int, int>();

        int messageIndex = 0;
        public int MessageIndex
        {
            get
            {
                return this.messageIndex;
            }

            set
            {
                this.messageIndex = value;
            }
        }

        Dictionary<int, int> ackQueue = new Dictionary<int, int>();
        Dictionary<int, int> nackQueue = new Dictionary<int, int>();

        Queue<Packet> packetQueue = new Queue<Packet>();

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
                DataPacket packet = (DataPacket) pk;
                packet.Decode();

                this.timedOut = 1000;

                if (packet.SeqNumber < this.startSeq || packet.SeqNumber > this.endSeq || this.receivedWindow.ContainsKey(packet.SeqNumber))
                {
                    return;
                }

                int diff = packet.SeqNumber - this.lastSeqNumber;

                if (this.nackQueue.ContainsKey(packet.SeqNumber))
                {
                    this.nackQueue.Remove(packet.SeqNumber);
                }

                if (!this.receivedWindow.ContainsKey(packet.SeqNumber))
                {
                    this.receivedWindow.Add(packet.SeqNumber, packet.SeqNumber);
                }

                if (!this.ackQueue.ContainsKey(packet.SeqNumber))
                {
                    this.ackQueue.Add(packet.SeqNumber, packet.SeqNumber);
                }

                if (diff != 1)
                {
                    for (int i = 0; i < diff; ++i)
                    {
                        if (!this.receivedWindow.ContainsKey(packet.SeqNumber - i))
                        {
                            if (!this.nackQueue.ContainsKey(packet.SeqNumber - i))
                            {
                                this.nackQueue.Add(packet.SeqNumber - i, packet.SeqNumber - i);
                            }
                        }
                    }
                }

                if (diff >= 1)
                {
                    this.lastSeqNumber = packet.SeqNumber;
                    this.startSeq += diff;
                    this.endSeq += diff;
                }

                for (int i = 0; i < packet.Packets.Length; ++i)
                {
                    if (packet.Packets[i] is EncapsulatedPacket)
                    {
                        this.EncapsulatedPacketHandle((EncapsulatedPacket) packet.Packets[i]);
                    }
                }

                packet.Packets = null;
            }
            else
            {
                if (pk is ACK)
                {
                    Logger.Log("§aHandle ACK");
                }
                else if (pk is NACK)
                {
                    Logger.Log("§cHandle NACK");
                }
            }
        }

        private void EncapsulatedPacketHandle(EncapsulatedPacket packet)
        {
            EncapsulatedPacketHandler(packet);
        }

        private void EncapsulatedPacketHandler(EncapsulatedPacket packet)
        {
            int id = packet.buffer[0];
            if (id < 0x80)
            {
                if (id == CLIENT_DISCONNECT_DataPacket.ID)
                {
                    Close("ClientDisconnect", false);
                }
                else if (this.state == STATE_CONNECTING)
                {
                    if (id == CLIENT_CONNECT_DataPacket.ID)
                    {
                        CLIENT_CONNECT_DataPacket ccd = new CLIENT_CONNECT_DataPacket();
                        ccd.SetBuffer(packet.buffer);
                        ccd.Decode();

                        SERVER_HANDSHAKE_DataPacket shd = new SERVER_HANDSHAKE_DataPacket();
                        shd.EndPoint = this.point;
                        shd.SendPing = ccd.SendPing;
                        shd.SendPong = ccd.SendPing + 1000;
                        shd.Encode();

                        EncapsulatedPacket enc = new EncapsulatedPacket();
                        enc.buffer = shd.ToArray();
                        enc.reliability = PacketReliability.UNRELIABLE;

                        SendPacket(enc);
                    }
                    else if (id == CLIENT_HANDSHAKE_DataPacket.ID)
                    {
                        CLIENT_HANDSHAKE_DataPacket chd = new CLIENT_HANDSHAKE_DataPacket();
                        chd.SetBuffer(packet.buffer);
                        chd.Decode();

                        if (chd.EndPoint.Port == this.server.GetPort())
                        {
                            this.state = STATE_CONNECTED;
                        }
                    }
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
                    enc.buffer = pong.ToArray();
                    enc.reliability = PacketReliability.UNRELIABLE;

                    SendPacket(enc);
                }
            }
            else if (id == 0xfe && this.state == STATE_CONNECTED)
            {
                Server.Instance.NetworkManager.HandleBatchPacket(this, packet.buffer);
            }
        }

        void SendQueuePackets()
        {
            for (int i = 0; i < this.packetQueue.Count; ++i)
            {
                this.server.SendPacket(this.packetQueue.Dequeue(), this.point.Address, this.point.Port);
            }
        }

        public void SendPacket(EncapsulatedPacket packet, bool notQueue = false)
        {
            if (this.server != null)
            {
                if (notQueue)
                {
                    DataPacket_0 pk = new DataPacket_0();
                    pk.SeqNumber = this.sendSeqNumber++;
                    pk.Packets = new[]
                    {
                        packet
                    };

                    this.server.SendPacket(pk, this.point.Address, this.point.Port);
                    return;
                }
                else
                {
                    /*if (packet.buffer.Length + 4 > this.mtuSize)
                    {
                        byte[][] buffers = Binary.SplitBytes(packet.buffer, this.mtuSize - 60);
                        int splitID = ++this.splitID % 65536;
                        for (int i = 0; i < buffers.Length; ++i)
                        {
                            EncapsulatedPacket pk = new EncapsulatedPacket();
                            pk.splitID = splitID;
                            pk.hasSplit = true;
                            pk.splitCount = buffers.Length;
                            pk.reliability = packet.reliability;
                            pk.splitIndex = i;
                            if (i > 0)
                            {
                                pk.messageIndex = this.messageIndex++;
                            }
                            else
                            {
                                pk.messageIndex = this.messageIndex;
                            }

                            DataPacket_0 dp = new DataPacket_0();
                            dp.SeqNumber = this.sendSeqNumber++;
                            dp.Packets = new[]
                            {
                                pk
                            };

                            this.server.SendPacket(dp, this.point.Address, this.point.Port);

                            Logger.Log("Split");
                        }
                    }
                    else
                    {*/
                    DataPacket_0 pk = new DataPacket_0();
                    pk.SeqNumber = this.sendSeqNumber++;
                    pk.Packets = new[]
                    {
                        packet
                    };

                    this.packetQueue.Enqueue(pk);
                }
            }
        }

        internal void Update()
        {
            if (this.timedOut <= 0)
            {
                this.Close("disconnect.timeout");
            }
            this.timedOut--;

            if (this.ackQueue.Count > 0)
            {
                ACK ack = new ACK();
                ack.packets = this.ackQueue.Values.ToArray();
                this.server.SendPacket(ack, this.point.Address, this.point.Port);
                this.ackQueue.Clear();
            }

            if (this.nackQueue.Count > 0)
            {
                NACK nack = new NACK();
                nack.packets = this.nackQueue.Values.ToArray();
                this.server.SendPacket(nack, this.point.Address, this.point.Port);
                this.nackQueue.Clear();
            }

            int[] a = this.receivedWindow.Values.ToArray();
            for (int i = 0; i < this.receivedWindow.Values.Count; ++i)
            {
                int seq = a[i];
                if (seq < this.startSeq)
                {
                    if (this.receivedWindow.ContainsKey(seq))
                    {
                        this.receivedWindow.Remove(seq);
                    }
                }
                else
                {
                    break;
                }
            }

            SendQueuePackets();
            //recover pk
        }

        internal void Close(string msg, bool serverClose = true)
        {
            if (serverClose)
            {
                CLIENT_DISCONNECT_DataPacket pk = new CLIENT_DISCONNECT_DataPacket();
                pk.Encode();

                EncapsulatedPacket ep = new EncapsulatedPacket();
                ep.buffer = pk.ToArray();
                ep.reliability = PacketReliability.UNRELIABLE;

                SendPacket(ep);
            }

            Server.Instance.NetworkManager.RemovePlayer(RakNetServer.IPEndPointToID(this.point));
            this.server.RemoveSession(this.point, msg);
        }
    }
}
