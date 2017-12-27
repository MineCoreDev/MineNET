using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

	    /** @var int[] */
	    private List<int> ACKQueue = new List<int>();
	    /** @var int[] */
	    private List<int> NACKQueue = new List<int>();

	    /** @var DataPacket[] */
	    private List<DataPacket> recoveryQueue = new List<DataPacket>();

	    /** @var DataPacket[][] */
	    private List<List<DataPacket>> splitPackets = new List<List<DataPacket>>();

	    /** @var int[][] */
	    private List<List<int>> needACK = new List<List<int>>();

	    /** @var DataPacket */
	    private DataPacket sendQueue;

	    private int windowStart;
	    private List<EncapsulatedPacket> receivedWindow = new List<EncapsulatedPacket>();
	    private int windowEnd;

	    private int reliableWindowStart;
	    private int reliableWindowEnd;
	    private List<int> reliableWindow = new List<int>();
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



        public void Close()
        {
            var data = "\x60\x00\x08\x00\x00\x00\x00\x00\x00\x00\x15";
            //addEncapsulatedToQueue(EncapsulatedPacket::fromBinary($data)); //CLIENT_DISCONNECT packet 0x15
            //sendQueue();
            sessionManager = null;
        }
    }
}
