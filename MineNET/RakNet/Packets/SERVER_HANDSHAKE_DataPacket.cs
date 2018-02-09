using System.Net;

namespace MineNET.RakNet.Packets
{
    public class SERVER_HANDSHAKE_DataPacket : Packet
    {
        public const int ID = 0x10;

        public override byte PacketID
        {
            get
            {
                return ID;
            }
        }

        IPEndPoint endPoint;
        public IPEndPoint EndPoint
        {
            get
            {
                return endPoint;
            }

            set
            {
                endPoint = value;
            }
        }

        IPEndPoint[] systemEndPoints = new IPEndPoint[]
        {
            new IPEndPoint(IPAddress.Parse("127.0.0.1"), 0),
            new IPEndPoint(IPAddress.Parse("0.0.0.0"), 0),
            new IPEndPoint(IPAddress.Parse("0.0.0.0"), 0),
            new IPEndPoint(IPAddress.Parse("0.0.0.0"), 0),
            new IPEndPoint(IPAddress.Parse("0.0.0.0"), 0),
            new IPEndPoint(IPAddress.Parse("0.0.0.0"), 0),
            new IPEndPoint(IPAddress.Parse("0.0.0.0"), 0),
            new IPEndPoint(IPAddress.Parse("0.0.0.0"), 0),
            new IPEndPoint(IPAddress.Parse("0.0.0.0"), 0),
            new IPEndPoint(IPAddress.Parse("0.0.0.0"), 0)
        };
        public IPEndPoint[] SystemEndPoint
        {
            get
            {
                return systemEndPoints;
            }

            set
            {
                systemEndPoints = value;
            }
        }

        long sendPing;
        public long SendPing
        {
            get
            {
                return sendPing;
            }

            set
            {
                sendPing = value;
            }
        }

        long sendPong;
        public long SendPong
        {
            get
            {
                return sendPong;
            }

            set
            {
                sendPong = value;
            }
        }

        public override void Encode()
        {
            base.Encode();

            WriteIPEndPoint(endPoint);
            WriteByte(0);

            for (int i = 0; i < 10; ++i)
            {
                WriteIPEndPoint(systemEndPoints[i]);
            }

            WriteLong(sendPing);
            WriteLong(sendPong);
        }
    }
}
