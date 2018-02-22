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
        SortedList<int, EncapsulatedPacket> reliableWindow = new SortedList<int, EncapsulatedPacket>();
        SortedList<int, int> receivedWindow = new SortedList<int, int>();

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

        SortedList<int, int> ackQueue = new SortedList<int, int>();
        SortedList<int, int> nackQueue = new SortedList<int, int>();

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
                    Logger.Log("BlockDataPacket");
                    return;
                }

                int diff = packet.SeqNumber - lastSeqNumber;

                if (nackQueue.ContainsKey(packet.SeqNumber))
                {
                    nackQueue.Remove(packet.SeqNumber);
                }
                receivedWindow[packet.SeqNumber] = packet.SeqNumber;

                if (diff != 1)
                {
                    for (int i = lastSeqNumber + 1; i < packet.SeqNumber; ++i)
                    {
                        if (!receivedWindow.ContainsKey(i))
                        {
                            nackQueue[i] = i;
                        }
                    }
                }

                ackQueue[packet.SeqNumber] = packet.SeqNumber;

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
                    Logger.Log("BlockEncPK{0}:{1}", startMsg, endMsg);
                    return;
                }

                if ((packet.messageIndex - lastMsg) == 1)
                {
                    ++lastMsg;
                    ++endMsg;
                    ++startMsg;

                    EncapsulatedPacketHandler(packet);

                    if (reliableWindow.Count > 0)
                    {
                        for (int i = 0; i < reliableWindow.Values.Count; ++i)
                        {
                            EncapsulatedPacket pk = reliableWindow.Values[i];
                            if ((pk.messageIndex - lastMsg) != 1)
                            {
                                break;
                            }

                            ++lastMsg;
                            ++endMsg;
                            ++startMsg;

                            EncapsulatedPacketHandler(pk);
                            reliableWindow.Remove(pk.messageIndex);
                        }
                    }
                }
                else
                {
                    /*if (!reliableWindow.ContainsKey(packet.messageIndex))
                    {
                        reliableWindow.Add(packet.messageIndex, packet);
                    }
                    else
                    {
                        reliableWindow[]
                    }*/
                    reliableWindow[packet.messageIndex] = packet;
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

                server.SendPacket(pk, point.Address, point.Port);
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
                nack.packets = ackQueue.Values.ToArray();
                server.SendPacket(nack, point.Address, point.Port);
                nackQueue.Clear();
            }

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
