using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

using MineCraftPENetwork.Protocol;

namespace MineCraftPENetwork.Server
{
    public class Session
    {
        const int STATE_CONNECTING = 0;
        const int STATE_CONNECTED = 1;

        const int MAX_SPLIT_SIZE = 128;
        const int MAX_SPLIT_COUNT = 4;

        public static int WINDOW_SIZE = 2048;

        private int messageIndex = 0;
	    private List<int> channelIndex = new List<int>();
        
	    private SessionManager sessionManager;
	    private string address;
	    private int port;
	    private int state = STATE_CONNECTING;
	    private int mtuSize;
	    private long id = 0;
	    private int splitID = 0;

	    private int sendSeqNumber = 0;
	    private int lastSeqNumber = -1;

	    private long lastUpdate;
	    private long startTime;

	    private bool isTemporal = true;
        
	    private List<DataPacket> packetToSend = new List<DataPacket>();

	    private bool isActive;

	    private Dictionary<int, int> ACKQueue = new Dictionary<int, int>();
	    private Dictionary<int, int> NACKQueue = new Dictionary<int, int>();
        
	    private Dictionary<int, dynamic> recoveryQueue = new Dictionary<int, dynamic>();

	    /** @var DataPacket[][] */
	    private Dictionary<int, Dictionary<int, EncapsulatedPacket>> splitPackets = new Dictionary<int, Dictionary<int, EncapsulatedPacket>>();

	    /** @var int[][] */
	    private List<List<int>> needACK = new List<List<int>>();

	    /** @var DataPacket */
	    private DataPacket sendQueue;

	    private int windowStart;
	    private Dictionary<int, int> receivedWindow = new Dictionary<int, int>();
	    private int windowEnd;

	    private int reliableWindowStart;
	    private int reliableWindowEnd;
	    private Dictionary<int, EncapsulatedPacket> reliableWindow = new Dictionary<int, EncapsulatedPacket>();
	    private int lastReliableIndex = -1;

        public Session(SessionManager mgr, string ip, int port, long clientID, int mtuSize)
        {
            sessionManager = mgr;
            address = ip;
            this.port = port;
            id = clientID;
            sendQueue = new DATA_PACKET_4();
		    lastUpdate = DateTime.Now.Ticks;
		    startTime = DateTime.Now.Ticks;
		    isActive = false;
		    windowStart = -1;
		    windowEnd = WINDOW_SIZE;

		    reliableWindowStart = 0;
		    reliableWindowEnd = WINDOW_SIZE;

            for (int i = 0; i < 32; ++i) {
                channelIndex.Add(0);
            }

            this.mtuSize = mtuSize;
        }

        public string GetAddress()
        {
            return address;
        }

        public int GetPort()
        {
            return port;
        }

        public long GetID()
        {
            return id;
        }

        public bool IsTemporal()
        {
            return isTemporal;
        }

        public void Update(long time)
        {
            if (!isActive && (lastUpdate + 50000000) < time) {
                Disconnect("timeout");

                return;
            }
		    isActive = false;

            if (ACKQueue.Count > 0)
            {
                var pk = new ACK();
                pk.packets = ACKQueue;
                SendPacket(pk);
                ACKQueue.Clear();
            }

            if (NACKQueue.Count > 0)
            {
                var pk = new NACK();
                pk.packets = NACKQueue;
                SendPacket(pk);
                NACKQueue.Clear();
            }

            if (packetToSend.Count > 0)
            {
                var limit = 16;
                for (var i = 0; i < packetToSend.Count - 1; i++)
                {
                    var pk = packetToSend[i];
                    pk.sendTime = time;
                    pk.Encode();
                    if (!recoveryQueue.ContainsKey(pk.seqNumber))
                    {
                        recoveryQueue.Add(pk.seqNumber, pk);
                    }
                    packetToSend.RemoveAt(i);
                    SendPacket(pk);

                    if (--limit <= 0)
                    {
                        break;
                    }
                }

                if (packetToSend.Count > WINDOW_SIZE)
                {
                    packetToSend.Clear();
                }
            }

            if (needACK.Count > 0)
            {
                for (var i = 0; i < needACK.Count - 1; i++)
                {
                    var indexes = needACK[i];
                    if (indexes.Count == 0)
                    {
                        needACK.RemoveAt(i);
                        sessionManager.NotifyACK(this, i);
                    }
                }
            }

            for (var i = 0; i < recoveryQueue.Count - 1; i++)
            {
                var seq = recoveryQueue.Keys.ToArray()[i];
                var pk = recoveryQueue.Values.ToArray()[i];

                if (pk.sendTime < (DateTime.Now.Ticks - 8))
                {
                    packetToSend.Add(pk);
                    recoveryQueue.Remove(seq);
                }
                else
                {
                    break;
                }
            }

            for (var i = 0; i < receivedWindow.Count - 1; i++)
            {
                var seq = receivedWindow.Keys.ToArray()[i];
                if (seq < windowStart)
                {
                    receivedWindow.Remove(seq);
                }
                else
                {
                    break;
                }
            }

		    SendQueue();
        }

        public void Disconnect(string reason = "unknown")
        {
            Console.WriteLine("[Session]" + reason);
            if (sessionManager != null)
            {
                sessionManager.RemoveSession(this, reason);
            }
        }

        private void SendPacket(Packet packet)
        {
            if (sessionManager != null)
            {
                sessionManager.SendPacket(packet, IPAddress.Parse(address), port);
            }
        }

        public void SendQueue()
        {
            if (sendQueue.packets.Count > 0)
            {
			    sendQueue.seqNumber = sendSeqNumber++;
			    SendPacket(sendQueue);
			    sendQueue.sendTime = DateTime.Now.Ticks;
			    recoveryQueue[sendQueue.seqNumber] = sendQueue;
			    sendQueue = new DATA_PACKET_4();
            }
        }

        private void AddToQueue(EncapsulatedPacket pk, int flags = RakNet.PRIORITY_NORMAL)
        {
		    var priority = flags & 0x07;
            if (pk.needACK && pk.messageIndex != -1)
            {
			    needACK[pk.identifierACK][pk.messageIndex] = pk.messageIndex;
            }
            if (priority == RakNet.PRIORITY_IMMEDIATE)
            { //Skip queues
                var packet = new DATA_PACKET_0();
                packet.seqNumber = sendSeqNumber++;
                if (pk.needACK) {
                    packet.packets.Add(pk);
                    pk.needACK = false;
                } else {
                    packet.packets.Add(pk);
                }

                SendPacket(packet);
                packet.sendTime = DateTime.Now.Ticks;
                recoveryQueue[packet.seqNumber] = packet;

                return;
            }
            var length = sendQueue.Length();
            if (length > mtuSize)
            {
			    SendQueue();
            }

            if (pk.needACK)
            {
                sendQueue.packets.Add(pk.Clone());
                pk.needACK = false;
            }
            else
            {
                sendQueue.packets.Add(pk.Clone());
            }
        }

        /**
         * @param EncapsulatedPacket packet
         * @param int                flags
         */
        public void AddEncapsulatedToQueue(EncapsulatedPacket packet, int flags = RakNet.PRIORITY_NORMAL)
        {

            if ((packet.needACK = (flags & RakNet.FLAG_NEED_ACK) > 0) == true)
            {
			    needACK[packet.identifierACK].Clear();
            }

            if (
                packet.reliability == PacketReliability.RELIABLE ||
                packet.reliability == PacketReliability.RELIABLE_ORDERED ||
                packet.reliability == PacketReliability.RELIABLE_SEQUENCED ||
                packet.reliability == PacketReliability.RELIABLE_WITH_ACK_RECEIPT ||
                packet.reliability == PacketReliability.RELIABLE_ORDERED_WITH_ACK_RECEIPT
            ) {
                packet.messageIndex = messageIndex++;

                if (packet.reliability == PacketReliability.RELIABLE_ORDERED)
                {
                    packet.orderIndex = channelIndex[packet.orderChannel]++;
                }
            }

            if (packet.GetTotalLength() + 4 > mtuSize) {
                //IP header size (20 bytes) + UDP header size (8 bytes) + RakNet weird (8 bytes) + datagram header size (4 bytes) + max encapsulated packet header size (20 bytes)
                var buffers = RakNet.SplitBuffer(packet.buffer, mtuSize - 60);
                var splitID = ++this.splitID % 65536;
                var count = 0;
                foreach (var buffer in buffers)
                {
                    var pk = new EncapsulatedPacket();
                    pk.splitID = splitID;
                    pk.hasSplit = true;
                    pk.splitCount = buffers.Count;
                    pk.reliability = packet.reliability;
                    pk.splitIndex = count;
                    pk.buffer = buffer;
                    if (count > 0)
                    {
                        pk.messageIndex = messageIndex++;
                    }
                    else
                    {
                        pk.messageIndex = packet.messageIndex;
                    }

                    if (pk.reliability == PacketReliability.RELIABLE_ORDERED)
                    {
                        pk.orderChannel = packet.orderChannel;
                        pk.orderIndex = packet.orderIndex;
                    }
                    AddToQueue(pk, flags | RakNet.PRIORITY_IMMEDIATE);
                    count++;
                }
            }
            else
            {
                AddToQueue(packet, flags);
            }
        }

        private void HandleSplit(EncapsulatedPacket packet)
        {
            if (packet.splitCount >= MAX_SPLIT_SIZE || packet.splitIndex >= MAX_SPLIT_SIZE || packet.splitIndex < 0){
                return;
            }


            if (!splitPackets.ContainsKey(packet.splitID))
            {
                if (splitPackets.Count >= MAX_SPLIT_COUNT)
                {
                    return;
                }
                splitPackets[packet.splitID].Add(packet.splitIndex, packet);
            }
            else
            {
                splitPackets[packet.splitID][packet.splitIndex] = packet;
            }

            if (splitPackets[packet.splitID].Count == packet.splitCount)
            {
                var pk = new EncapsulatedPacket();
                var buffer = new List<byte>();
                for (var i = 0; i < packet.splitCount; ++i) {
                    buffer.AddRange(splitPackets[packet.splitID][i].buffer);
                }
                pk.buffer = buffer.ToArray();

                pk.length = buffer.Count;
                splitPackets.Remove(packet.splitID);

                Console.WriteLine("[Info]Split Finish");

                HandleEncapsulatedPacketRoute(pk);
            }
        }

        private void HandleEncapsulatedPacket(EncapsulatedPacket packet)
        {
            if (packet.messageIndex == -1){
			    HandleEncapsulatedPacketRoute(packet);
            }
            else
            {
                if (packet.messageIndex < reliableWindowStart || packet.messageIndex > reliableWindowEnd)
                {
                    return;
                }

                if ((packet.messageIndex - lastReliableIndex) == 1)
                {
                    lastReliableIndex++;
                    reliableWindowStart++;
                    reliableWindowEnd++;
                    HandleEncapsulatedPacketRoute(packet);

                    if (reliableWindow.Count > 0)
                    {
                        reliableWindow.OrderBy((x) => x.Value);
                        for (var i = 0; i < reliableWindow.Count - 1; i++)
                        {
                            var pk = reliableWindow[i];
                            if ((i - lastReliableIndex) != 1)
                            {
                                break;
                            }
                            lastReliableIndex++;
                            reliableWindowStart++;
                            reliableWindowEnd++;
                            HandleEncapsulatedPacketRoute(pk);
                            reliableWindow.Remove(i);
                        }
                    }
                }
                else
                {
                    reliableWindow[packet.messageIndex] = packet;
                }
            }

        }

        public int GetState()
        {
            return state;
        }

        private void HandleEncapsulatedPacketRoute(EncapsulatedPacket packet)
        {
            if (sessionManager == null || packet.buffer.Length < 0)
            {
                return;
            }

            if (packet.hasSplit)
            {
                if (state == STATE_CONNECTED)
                {
                    HandleSplit(packet);
                    Console.WriteLine("[SplitPacket]");
                }

                return;
            }

            var id = packet.buffer[0];
            Console.WriteLine("[EncapsulatedPacket]<pid>" + id + "<len>" + packet.buffer.Length);

            if (id < 0x80)
            { //internal data packet
                if (state == STATE_CONNECTING)
                {
                    if (id == CLIENT_CONNECT_DataPacket.ID)
                    {
                        var dataPacket = new CLIENT_CONNECT_DataPacket();
                        dataPacket.Buffer = packet.buffer;
                        dataPacket.Decode();

                        var pk = new SERVER_HANDSHAKE_DataPacket();
                        pk.address = address;
                        pk.port = port;
                        pk.sendPing = dataPacket.sendPing;
                        pk.sendPong = pk.sendPing + 1000;
                        pk.Encode();

                        var sendPacket = new EncapsulatedPacket();
                        sendPacket.reliability = PacketReliability.UNRELIABLE;
                        sendPacket.buffer = pk.Buffer;
                        AddToQueue(sendPacket, RakNet.PRIORITY_IMMEDIATE);
                        
                    }
                    else if (id == CLIENT_HANDSHAKE_DataPacket.ID)
                    {
                        var dataPacket = new CLIENT_HANDSHAKE_DataPacket();
                        dataPacket.Buffer = packet.buffer;
                        dataPacket.Decode();

                        if (dataPacket.port == sessionManager.GetPort() || !sessionManager.portChecking)
                        {
                            state = STATE_CONNECTED; //FINALLY!
                            isTemporal = false;
                            sessionManager.OpenSession(this);
                        }
                    }
                }
                else if (id == CLIENT_DISCONNECT_DataPacket.ID)
                {
                    Disconnect("client disconnect");
                }
                else if (id == PING_DataPacket.ID)
                {
                    var dataPacket = new PING_DataPacket();
                    dataPacket.Buffer = packet.buffer;
                    dataPacket.Decode();

                    var pk = new PONG_DataPacket();
                    pk.pingID = dataPacket.pingID;
                    pk.Encode();

                    var sendPacket = new EncapsulatedPacket();
                    sendPacket.reliability = PacketReliability.UNRELIABLE;
                    sendPacket.buffer = pk.Buffer;
                    AddToQueue(sendPacket);
                }//TODO: add PING/PONG (0x00/0x03) automatic latency measure
            }
            else if (state == STATE_CONNECTED)
            {
                sessionManager.StreamEncapsulated(this, packet);
                Console.WriteLine("Test");
                //TODO: stream channels
            } else {
                //sessionManager.getLogger().notice("Received packet before connection: " . bin2hex(packet.buffer));
            }
	}

        public void HandlePacket(Packet packet) {
            isActive = true;
            lastUpdate = DateTime.Now.Ticks;

            if (packet.PacketID >= 0x80 && packet.PacketID <= 0x8f && packet is DataPacket)
            {
                var tc = (DataPacket)packet;
                tc.Decode();

                if (tc.seqNumber < windowStart || tc.seqNumber > windowEnd || receivedWindow.ContainsKey(tc.seqNumber)){
                    return;
                }

                var diff = tc.seqNumber - lastSeqNumber;

                NACKQueue.Remove(tc.seqNumber);
                if (!ACKQueue.ContainsKey(tc.seqNumber))
                {
                    ACKQueue.Add(tc.seqNumber, tc.seqNumber);
                }
                receivedWindow.Add(tc.seqNumber, tc.seqNumber);

                if (diff != 1)
                {
                    for (var i = lastSeqNumber + 1; i < tc.seqNumber; ++i)
                    {
                        if (!receivedWindow.ContainsKey(i))
                        {
                            NACKQueue.Add(i, i);
                        }
                    }
                }

                if (diff >= 1) {
                    lastSeqNumber = tc.seqNumber;
                    windowStart += diff;
                    windowEnd += diff;
                }

                foreach (var pk in tc.packets)
                {
                    if (pk is EncapsulatedPacket)
                    {
                        HandleEncapsulatedPacket((EncapsulatedPacket)pk);
                    }
                }
            }
            else
            {
                if (packet is ACK)
                {
                    var tc = (ACK)packet;
                    tc.Decode();
                    foreach (var seq in tc.packets)
                    {
                        if (recoveryQueue.ContainsKey(seq.Value))
                        {
                            foreach (var pk in recoveryQueue[seq.Value].packets)
                            {
                                if (pk is EncapsulatedPacket && pk.needACK && pk.messageIndex != -1)
                                {
                                    needACK[pk.identifierACK].RemoveAt(pk.messageIndex);
                                }
                            }
                            recoveryQueue.Remove(seq.Value);
                        }
                    }
                }
                else if (packet is NACK)
                {
                    var tc = (NACK)packet;
                    packet.Decode();
                    foreach (var seq in tc.packets)
                    {
                        if (recoveryQueue.ContainsKey(seq.Value)) {
                            var pk = recoveryQueue[seq.Value];
                            pk.seqNumber = sendSeqNumber++;
                            packetToSend.Add(pk);
                            recoveryQueue.Remove(seq.Value);
                        }
                    }
                }
            }
        }

        public void Close()
        {
            var data = new List<byte>()
            {
                0x60,
                0x00,
                0x08,
                0x00,
                0x00,
                0x00,
                0x00,
                0x00,
                0x00,
                0x00,
                0x15
            };
            var offset = 0;
            //AddEncapsulatedToQueue(EncapsulatedPacket.FromBinary(data.ToArray(), ref offset)); //CLIENT_DISCONNECT packet 0x15
            //SendQueue();
            sessionManager = null;
        }
    }
}
