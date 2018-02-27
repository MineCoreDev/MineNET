using System.Collections.Generic;
using System.Linq;
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
        int startSeq = -1;
        int endSeq = 2048;
        int lastSeqNumber = -1;

        int startMsg = 0;
        int endMsg = 2048;
        int lastMsg = -1;
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

                this.timedOut = 100;

                if (packet.SeqNumber < startSeq || packet.SeqNumber > endSeq || receivedWindow.ContainsKey(packet.SeqNumber))
                {
                    return;
                }

                int diff = packet.SeqNumber - lastSeqNumber;

                if (nackQueue.ContainsKey(packet.SeqNumber))
                {
                    nackQueue.Remove(packet.SeqNumber);
                }

                if (!receivedWindow.ContainsKey(packet.SeqNumber))
                {
                    receivedWindow.Add(packet.SeqNumber, packet.SeqNumber);
                }

                if (!ackQueue.ContainsKey(packet.SeqNumber))
                {
                    ackQueue.Add(packet.SeqNumber, packet.SeqNumber);
                }

                if (diff != 1)
                {
                    for (int i = lastSeqNumber + 1; i < packet.SeqNumber; ++i)
                    {
                        if (!receivedWindow.ContainsKey(i))
                        {
                            if (!nackQueue.ContainsKey(i))
                            {
                                nackQueue.Add(i, i);
                            }
                        }
                    }
                }

                if (diff >= 1)
                {
                    lastSeqNumber = packet.SeqNumber;
                    startSeq += diff;
                    endSeq += diff;
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
            if (packet.messageIndex != -1)
            {
                if (packet.messageIndex < startMsg || packet.messageIndex > endMsg)
                {
                    return;
                }

                if ((packet.messageIndex - lastMsg) == 1)
                {
                    lastMsg++;
                    endMsg++;
                    startMsg++;

                    EncapsulatedPacketHandler(packet);

                    if (reliableWindow.Count > 0)
                    {
                        List<KeyValuePair<int, EncapsulatedPacket>> l = new List<KeyValuePair<int, EncapsulatedPacket>>(reliableWindow);
                        List<int> removeIndex = new List<int>();
                        l.Sort((a, b) => a.Key - b.Key);
                        KeyValuePair<int, EncapsulatedPacket>[] pks = l.ToArray();
                        for (int i = 0; i < reliableWindow.Count; ++i)
                        {
                            EncapsulatedPacket pk = pks[i].Value;
                            if ((pk.messageIndex - lastMsg) != 1)
                            {
                                break;
                            }

                            EncapsulatedPacketHandler(pk);

                            lastMsg++;
                            endMsg++;
                            startMsg++;

                            removeIndex.Add(pk.messageIndex);
                        }

                        for (int i = 0; i < removeIndex.Count; ++i)
                        {
                            reliableWindow.Remove(removeIndex[i]);
                        }
                    }
                }
                else
                {
                    if (!reliableWindow.ContainsKey(packet.messageIndex))
                    {
                        reliableWindow.Add(packet.messageIndex, packet);
                    }
                    else
                    {
                        reliableWindow[packet.messageIndex] = packet;
                    }
                }
            }
            else
            {
                EncapsulatedPacketHandler(packet);
            }
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
                else if (state == STATE_CONNECTING)
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
                Server.Instance.NetworkManager.HandleBatchPacket(this, packet.buffer);
            }
        }

        void SendQueuePackets()
        {
            for (int i = 0; i < packetQueue.Count; ++i)
            {
                server.SendPacket(packetQueue.Dequeue(), point.Address, point.Port);
            }
        }

        public void SendPacket(EncapsulatedPacket packet)
        {
            if (server != null)
            {
                DataPacket_0 pk = new DataPacket_0();
                pk.SeqNumber = sendSeqNumber++;
                pk.Packets = new[]
                {
                    packet
                };

                packetQueue.Enqueue(pk);
            }
        }

        internal void Update()
        {
            if (this.timedOut <= 0)
            {
                this.Close("timedout");
            }
            this.timedOut--;

            if (ackQueue.Count > 0)
            {
                ACK ack = new ACK();
                ack.packets = ackQueue.Values.ToArray();
                server.SendPacket(ack, point.Address, point.Port);
                ackQueue.Clear();
            }

            if (nackQueue.Count > 0)
            {
                NACK nack = new NACK();
                nack.packets = nackQueue.Values.ToArray();
                server.SendPacket(nack, point.Address, point.Port);

                int[] datas = nackQueue.Values.ToArray();
                for (int i = 0; i < nackQueue.Count; ++i)
                {
                    if (receivedWindow.ContainsKey(datas[i]))
                    {
                        nackQueue.Remove(i);
                    }
                }
            }

            int[] a = receivedWindow.Values.ToArray();
            for (int i = 0; i < receivedWindow.Values.Count; ++i)
            {
                int seq = a[i];
                if (seq < startSeq)
                {
                    if (receivedWindow.ContainsKey(seq))
                    {
                        receivedWindow.Remove(seq);
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
                ep.buffer = pk.GetResult();
                ep.reliability = PacketReliability.UNRELIABLE;

                SendPacket(ep);
            }

            Server.Instance.NetworkManager.RemovePlayer(RakNetServer.IPEndPointToID(point));
            this.server.RemoveSession(this.point, msg);
        }
    }
}
