﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO.Compression;
using System.Net;
using MineNET.Entities.Players;
using MineNET.Events.NetworkEvents;
using MineNET.Events.NetworkEvents.RakNet;
using MineNET.IO;
using MineNET.Network.MinecraftPackets;
using MineNET.Network.RakNetPackets;
using MineNET.Utils;

namespace MineNET.Network
{
    public class NetworkSession
    {
        public static int WindowSize { get; } = 2048;
        public static int TimedOutTime { get; } = 3000;
        public static int SendTimedOut { get; } = 500;
        public static int SendPingTime { get; } = 5000;

        public IPEndPoint EndPoint { get; }
        public long ClientID { get; }
        public short MTUSize { get; }

        public NetworkManager Manager { get; private set; }

        public int LastUpdateTime { get; private set; }
        public int LastSendTime { get; private set; }
        public int LastPingTime { get; private set; } = SendPingTime;

        public int MessageIndex { get; private set; }

        public ConcurrentDictionary<int, int> ACKQueue { get; private set; } = new ConcurrentDictionary<int, int>();
        public ConcurrentDictionary<int, int> NACKQueue { get; private set; } = new ConcurrentDictionary<int, int>();

        public int WindowStart { get; private set; }
        public int WindowEnd { get; private set; } = WindowSize;

        public ConcurrentDictionary<int, int> ReceivedWindow { get; private set; } =
            new ConcurrentDictionary<int, int>();

        public int LastSeqNumber { get; private set; } = -1;
        public int LastSendSeqNumber { get; private set; } = 0;

        public int SplitID { get; private set; }
        public ConcurrentDictionary<int, int> OrderIndexes { get; private set; } = new ConcurrentDictionary<int, int>();

        public DataPacket SendQueue { get; private set; } = new DataPacket4();

        public ConcurrentDictionary<int, ConcurrentDictionary<int, EncapsulatedPacket>> SplitPackets { get; set; } =
            new ConcurrentDictionary<int, ConcurrentDictionary<int, EncapsulatedPacket>>();

        public int ReliableWindowStart { get; private set; }
        public int ReliableWindowEnd { get; private set; } = WindowSize;

        public ConcurrentDictionary<int, bool> ReliableWindow { get; private set; } =
            new ConcurrentDictionary<int, bool>();

        public ConcurrentDictionary<int, DataPacket> SendedPacket { get; private set; } =
            new ConcurrentDictionary<int, DataPacket>();

        public ConcurrentQueue<DataPacket> ResendQueue { get; set; } = new ConcurrentQueue<DataPacket>();

        public SessionState State { get; private set; } = SessionState.Connecting;

        public NetworkSession(IPEndPoint endPoint, long clientID, short mtuSize)
        {
            this.Manager = Server.Instance.Network;

            this.EndPoint = endPoint;
            this.ClientID = clientID;
            this.MTUSize = mtuSize;

            this.LastUpdateTime = TimedOutTime;

            for (int i = 0; i < 32; i++)
            {
                this.OrderIndexes.TryAdd(i, 0);
            }
        }

        public void OnUpdate()
        {
            if (this.LastUpdateTime < 0)
            {
                this.Disconnect("timedout");
                return;
            }

            if (this.ACKQueue.Count > 0)
            {
                Ack pk = new Ack();
                List<int> pks = new List<int>();
                foreach (KeyValuePair<int, int> kv in this.ACKQueue.ToArray())
                {
                    pks.Add(kv.Value);
                }

                pks.Sort();

                pk.Packets = pks.ToArray();
                this.SendPacket(pk);

                this.ACKQueue.Clear();
            }

            if (this.NACKQueue.Count > 0)
            {
                Nack pk = new Nack();
                List<int> pks = new List<int>();
                foreach (KeyValuePair<int, int> kv in this.NACKQueue.ToArray())
                {
                    pks.Add(kv.Value);
                }

                pks.Sort();

                pk.Packets = pks.ToArray();
                this.SendPacket(pk);

                this.NACKQueue.Clear();
            }

            if (this.ResendQueue.Count > 0)
            {
                for (int i = 0; i < this.ResendQueue.Count; ++i)
                {
                    DataPacket pk;
                    this.ResendQueue.TryDequeue(out pk);
                    pk.SeqNumber = this.LastSendSeqNumber++;
                    this.SendPacket(pk);

                    Logger.Debug("%server.network.dataPacket.resend");
                }
            }

            if (this.SendedPacket.Count > 0)
            {
                foreach (DataPacket pk in this.SendedPacket.Values)
                {
                    if (pk.SendTimedOut < 0)
                    {
                        DataPacket remove;
                        this.ResendQueue.Enqueue(pk);
                        this.SendedPacket.TryRemove(pk.SeqNumber, out remove);
                    }

                    --pk.SendTimedOut;
                }
            }

            if (this.LastPingTime < 0)
            {
                OnlinePing ping = new OnlinePing();
                ping.PingID = DateTime.Now.Ticks;

                this.QueueConnectedPacket(ping, RakNetPacketReliability.UNRELIABLE, -1, RakNetProtocol.FlagImmediate);
            }

            this.SendQueuePacket();

            --this.LastPingTime;
            --this.LastUpdateTime;
        }

        #region Handle Packet Method

        public void HandleDataPacket(DataPacket packet)
        {
            if (packet.SeqNumber < this.WindowStart || packet.SeqNumber > this.WindowEnd ||
                this.ACKQueue.ContainsKey(packet.SeqNumber))
            {
                return;
            }

            RakNetDataPacketReceiveEventArgs ev = new RakNetDataPacketReceiveEventArgs(this, packet);
            Server.Instance.Event.Network.OnRakNetDataPacketReceive(this, ev);

            if (ev.IsCancel)
            {
                return;
            }

            packet = ev.Packet;

            this.LastUpdateTime = this.LastUpdateTime = TimedOutTime;

            if (this.NACKQueue.ContainsKey(packet.SeqNumber))
            {
                int i;
                this.NACKQueue.TryRemove(packet.SeqNumber, out i);
            }

            this.ACKQueue.TryAdd(packet.SeqNumber, packet.SeqNumber);
            this.ReceivedWindow.TryAdd(packet.SeqNumber, packet.SeqNumber);

            int diff = packet.SeqNumber - this.LastSeqNumber;
            if (diff != 1)
            {
                for (int i = 0; i < diff; ++i)
                {
                    if (!this.ReceivedWindow.ContainsKey(packet.SeqNumber - i))
                    {
                        if (!this.NACKQueue.ContainsKey(packet.SeqNumber - i))
                        {
                            this.NACKQueue.TryAdd(packet.SeqNumber - i, packet.SeqNumber - i);
                        }
                    }
                }
            }

            if (diff >= 1)
            {
                this.LastSeqNumber = packet.SeqNumber;
                this.WindowStart += diff;
                this.WindowEnd += diff;
            }

            for (int i = 0; i < packet.Packets.Length; i++)
            {
                if (packet.Packets[i] is EncapsulatedPacket)
                {
                    this.HandleEncapsulatedPacket((EncapsulatedPacket) packet.Packets[i]);
                }
            }
        }

        public void HandleEncapsulatedPacket(EncapsulatedPacket packet)
        {
            if (packet.MessageIndex != -1)
            {
                if (packet.MessageIndex < this.ReliableWindowStart || packet.MessageIndex > this.ReliableWindowEnd ||
                    this.ReliableWindow.ContainsKey(packet.MessageIndex))
                {
                    return;
                }

                this.ReliableWindow[packet.MessageIndex] = true;

                if (packet.MessageIndex == this.ReliableWindowStart)
                {
                    for (; this.ReliableWindow.ContainsKey(this.ReliableWindowStart); ++this.ReliableWindowStart)
                    {
                        bool v;
                        this.ReliableWindow.TryRemove(this.ReliableWindowStart, out v);

                        ++this.ReliableWindowEnd;
                    }
                }

                if (packet.HasSplit && (packet = this.HandleSplit(packet)) == null)
                {
                    return;
                }

                RakNetEncapsulatedReceiveEventArgs ev = new RakNetEncapsulatedReceiveEventArgs(this, packet);
                Server.Instance.Event.Network.OnRakNetEncapsulatedReceive(this, ev);

                if (ev.IsCancel)
                {
                    return;
                }

                packet = ev.Packet;

                this.HandleEncapsulatedPacketRoute(packet);
            }
            else
            {
                this.HandleEncapsulatedPacketRoute(packet);
            }
        }

        public void HandleEncapsulatedPacketRoute(EncapsulatedPacket packet)
        {
            if (this.Manager == null)
            {
                return;
            }

            int id = packet.Buffer[0];
            RakNetPacket pk = this.Manager.GetRakNetPacket(id, packet.Buffer);
            if (pk != null)
            {
                if (id < 0x86)
                {
                    if (this.State == SessionState.Connecting)
                    {
                        if (id == RakNetProtocol.ClientConnectDataPacket)
                        {
                            ClientConnectDataPacket ccd = (ClientConnectDataPacket) pk;
                            ServerHandShakeDataPacket shd = new ServerHandShakeDataPacket();
                            shd.EndPoint = this.EndPoint;
                            shd.SendPing = ccd.SendPing;
                            shd.SendPong = ccd.SendPing + 1000;

                            this.QueueConnectedPacket(shd, RakNetPacketReliability.UNRELIABLE, -1,
                                RakNetProtocol.FlagImmediate);
                        }
                        else if (id == RakNetProtocol.ClientHandShakeDataPacket)
                        {
                            ClientHandShakeDataPacket chsd = (ClientHandShakeDataPacket) pk;

                            if (chsd.EndPoint.Port == Server.Instance.EndPoint.Port)
                            {
                                this.State = SessionState.Connected;
                            }
                        }
                    }
                    else if (id == RakNetProtocol.ClientDisconnectDataPacket)
                    {
                        this.Disconnect("clientDisconnect");
                    }
                    else if (id == RakNetProtocol.OnlinePing)
                    {
                        OnlinePing ping = (OnlinePing) pk;
                        OnlinePong pong = new OnlinePong();
                        pong.PingID = ping.PingID;

                        this.LastPingTime = SendPingTime;

                        this.QueueConnectedPacket(pong, RakNetPacketReliability.UNRELIABLE, -1,
                            RakNetProtocol.FlagImmediate);
                    }
                    else if (id == RakNetProtocol.OnlinePong)
                    {
                    }
                }
                else if (this.State == SessionState.Connected)
                {
                    if (id == RakNetProtocol.BatchPacket)
                    {
                        this.HandleBatchPacket((BatchPacket) pk);
                    }
                }
            }
        }

        public void HandleBatchPacket(BatchPacket packet)
        {
            string endPointStr = this.EndPoint.ToString();

            if (this.Manager.Players.ContainsKey(endPointStr))
            {
                Player player = this.Manager.Players[endPointStr];
                RakNetBatchPacketReceiveEventArgs ev = new RakNetBatchPacketReceiveEventArgs(this, player, packet);
                Server.Instance.Event.Network.OnRakNetBatchPacketReceive(this, ev);

                if (ev.IsCancel)
                {
                    return;
                }

                packet = ev.Packet;

                this.HandleMinecraftPacket(packet, player);
            }
        }

        public void HandleMinecraftPacket(BatchPacket pk, Player player)
        {
            using (BinaryStream stream = new BinaryStream(pk.Payload))
            {
                while (!stream.EndOfStream)
                {
                    int len = stream.ReadVarInt();
                    byte[] buffer = stream.ReadBytes(len);
                    MinecraftPacket packet = this.Manager.GetMinecraftPacket(buffer[0], buffer);
                    if (packet != null)
                    {
                        /*DataPacketReceiveArgs args = new DataPacketReceiveArgs(player, pk);
                        ServerEvents.OnPacketReceive(args);

                        if (args.IsCancel)
                        {
                            return;
                        }*/

                        if (Server.Instance.Config.PacketDebug)
                        {
                            Logger.Debug("%server.network.minecraft.receivePacket", buffer[0].ToString("X"),
                                buffer.Length);
                        }


                        Server.Instance.Invoke(() => player.OnPacketHandle(packet.Clone()));
                    }
                    else
                    {
                        if (Server.Instance.Config.PacketDebug)
                        {
                            Logger.Debug("%server.network.minecraft.notHandle", buffer[0].ToString("X"));
                        }
                    }
                }
            }
        }

        public EncapsulatedPacket HandleSplit(EncapsulatedPacket packet)
        {
            if (!this.SplitPackets.ContainsKey(packet.SplitID))
            {
                this.SplitPackets.TryAdd(packet.SplitID, new ConcurrentDictionary<int, EncapsulatedPacket>());
                if (!this.SplitPackets[packet.SplitID].ContainsKey(packet.SplitIndex))
                {
                    this.SplitPackets[packet.SplitID].TryAdd(packet.SplitIndex, packet);
                }
            }
            else
            {
                if (!this.SplitPackets[packet.SplitID].ContainsKey(packet.SplitIndex))
                {
                    this.SplitPackets[packet.SplitID].TryAdd(packet.SplitIndex, packet);
                }
            }

            if (this.SplitPackets[packet.SplitID].Count == packet.SplitCount)
            {
                EncapsulatedPacket pk = new EncapsulatedPacket();
                ConcurrentDictionary<int, EncapsulatedPacket> d;
                int offset = 0;
                pk.Buffer = new byte[0];
                for (int i = 0; i < packet.SplitCount; ++i)
                {
                    EncapsulatedPacket p = this.SplitPackets[packet.SplitID][i];
                    byte[] buffer = pk.Buffer;
                    Array.Resize(ref buffer, pk.Buffer.Length + p.Buffer.Length);
                    pk.Buffer = buffer;
                    Buffer.BlockCopy(p.Buffer, 0, pk.Buffer, offset, p.Buffer.Length);
                    offset += p.Buffer.Length;
                }

                pk.Length = pk.Buffer.Length;

                this.SplitPackets.TryRemove(pk.SplitID, out d);

                return pk;
            }

            return null;
        }

        public void HandleAcknowledgePacket(AcknowledgePacket packet)
        {
            if (packet is Ack)
            {
                foreach (int p in packet.Packets)
                {
                    DataPacket pk;
                    this.SendedPacket.TryRemove(p, out pk);
                }
            }
            else if (packet is Nack)
            {
                foreach (int p in packet.Packets)
                {
                    if (this.SendedPacket.ContainsKey(p))
                    {
                        DataPacket pk = this.SendedPacket[p];
                        DataPacket remove;
                        this.ResendQueue.Enqueue(pk);
                        this.SendedPacket.TryRemove(p, out remove);
                    }
                }
            }
        }

        #endregion

        #region Send Packet Method

        public void AddEncapsulatedToQueue(EncapsulatedPacket packet, int flags = RakNetProtocol.FlagNormal)
        {
            if (RakNetPacketReliability.IsReliable(packet.Reliability))
            {
                packet.MessageIndex = this.MessageIndex++;
                if (RakNetPacketReliability.IsOrdered(packet.Reliability))
                {
                    packet.OrderIndex = this.OrderIndexes[packet.OrderChannel]++;
                }
            }

            if (packet.GetTotalLength() + 4 > this.MTUSize)
            {
                byte[][] buffers = Binary.SplitBytes(new MemorySpan(packet.Buffer), this.MTUSize - 60);
                int splitID = ++this.SplitID % 65536;
                for (int i = 0; i < buffers.Length; i++)
                {
                    EncapsulatedPacket pk = new EncapsulatedPacket();
                    pk.SplitID = splitID;
                    pk.HasSplit = true;
                    pk.SplitCount = buffers.Length;
                    pk.Reliability = packet.Reliability;
                    pk.SplitIndex = i;
                    pk.Buffer = buffers[i];
                    if (i > 0)
                    {
                        pk.MessageIndex = this.MessageIndex++;
                    }
                    else
                    {
                        pk.MessageIndex = packet.MessageIndex;
                    }

                    if (RakNetPacketReliability.IsOrdered(packet.Reliability))
                    {
                        pk.OrderChannel = packet.OrderChannel;
                        pk.OrderIndex = packet.OrderIndex;
                    }

                    this.AddToQueue(pk, RakNetProtocol.FlagImmediate);
                }
            }
            else
            {
                this.AddToQueue(packet, flags);
            }
        }

        public void QueueConnectedPacket(RakNetPacket packet, int reliability, int orderChannel,
            int flags = RakNetProtocol.FlagNormal)
        {
            packet.Encode();

            EncapsulatedPacket pk = new EncapsulatedPacket();
            pk.Reliability = reliability;
            pk.OrderChannel = orderChannel;
            pk.Buffer = packet.ToArray();

            RakNetEncapsulatedSendEventArgs ev = new RakNetEncapsulatedSendEventArgs(this, pk);
            Server.Instance.Event.Network.OnRakNetEncapsulatedSend(this, ev);

            if (ev.IsCancel)
            {
                return;
            }

            pk = ev.Packet;

            this.AddEncapsulatedToQueue(pk, flags);
        }

        public void AddPacketBatchQueue(MinecraftPacket packet, int reliability, int flags = RakNetProtocol.FlagNormal)
        {
            packet.Encode();

            byte[] buffer = packet.ToArray();

            BinaryStream st = new BinaryStream();
            st.WriteVarInt((int) buffer.Length);
            st.WriteBytes(buffer);

            if (Server.Instance.Config.PacketDebug)
            {
                Logger.Debug("%server.network.minecraft.sendPacket", packet.PacketID.ToString("X"), packet.Length);
            }

            BatchPacket pk = new BatchPacket();
            if (packet is FullChunkDataPacket)
            {
                pk.CompressionLevel = CompressionLevel.Optimal;
            }

            pk.Payload = st.ToArray();

            string endPointStr = this.EndPoint.ToString();
            Player player = null;
            Server.Instance.Network.Players.TryGetValue(endPointStr, out player);
            if (player == null)
            {
                return;
            }

            RakNetBatchPacketSendEventArgs ev = new RakNetBatchPacketSendEventArgs(this, player, pk);
            Server.Instance.Event.Network.OnRakNetBatchPacketSend(this, ev);

            if (ev.IsCancel)
            {
                return;
            }

            pk = ev.Packet;

            this.QueueConnectedPacket(pk, reliability, packet.OrderChannel, flags);
        }

        public void AddToQueue(EncapsulatedPacket pk, int flags = RakNetProtocol.FlagNormal)
        {
            if (flags == RakNetProtocol.FlagImmediate)
            {
                DataPacket p = new DataPacket0();
                p.Packets = new object[] {pk};
                this.SendDatagram(p);
                return;
            }

            int length = this.SendQueue.GetLength();
            if (length + pk.GetTotalLength() > this.MTUSize - 36
            ) //IP header (20 bytes) + UDP header (8 bytes) + RakNet weird (8 bytes) = 36 bytes
            {
                this.SendQueuePacket();
            }

            List<object> list = new List<object>(this.SendQueue.Packets);
            list.Add(pk);
            this.SendQueue.Packets = list.ToArray();
        }

        public void SendQueuePacket()
        {
            if (this.SendQueue.Packets?.Length > 0)
            {
                this.SendDatagram(this.SendQueue);
                this.SendQueue = new DataPacket4();
            }
        }

        public void SendDatagram(DataPacket pk)
        {
            RakNetDataPacketSendEventArgs ev = new RakNetDataPacketSendEventArgs(this, pk);
            Server.Instance.Event.Network.OnRakNetDataPacketSend(this, ev);

            if (ev.IsCancel)
            {
                return;
            }

            pk = ev.Packet;

            pk.SeqNumber = this.LastSendSeqNumber++;
            this.SendedPacket.TryAdd(pk.SeqNumber, pk);
            this.SendPacket(pk.Clone());
        }

        public void SendPacket(RakNetPacket pk)
        {
            this.Manager?.Send(this.EndPoint, pk);
        }

        #endregion

        public void Disconnect(string reason)
        {
            this.State = SessionState.Disconnecting;
            Logger.Info("%server.network.raknet.sessionDisconnect", this.EndPoint, reason);

            this.Close();
        }

        public void Close()
        {
            if (this.State != SessionState.Disconnected)
            {
                this.State = SessionState.Disconnected;

                ClientDisconnectDataPacket pk = new ClientDisconnectDataPacket();
                this.QueueConnectedPacket(pk, RakNetPacketReliability.UNRELIABLE, -1, RakNetProtocol.FlagImmediate);

                CloseSessionEventArgs ev = new CloseSessionEventArgs(this.EndPoint, this);
                Server.Instance.Event.Network.OnCloseSession(this, ev);

                Logger.Debug("%server.network.raknet.sessionClose", this.EndPoint);
                this.Manager?.RemoveSession(this.EndPoint);
                this.Manager = null;
            }
        }
    }
}