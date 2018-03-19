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
                return this.endPoint;
            }

            set
            {
                this.endPoint = value;
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
            new IPEndPoint(IPAddress.Parse("0.0.0.0"), 0),
            new IPEndPoint(IPAddress.Parse("0.0.0.0"), 0),
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
                return this.systemEndPoints;
            }

            set
            {
                this.systemEndPoints = value;
            }
        }

        long sendPing;
        public long SendPing
        {
            get
            {
                return this.sendPing;
            }

            set
            {
                this.sendPing = value;
            }
        }

        long sendPong;
        public long SendPong
        {
            get
            {
                return this.sendPong;
            }

            set
            {
                this.sendPong = value;
            }
        }

        public override void Encode()
        {
            base.Encode();

            WriteIPEndPoint(this.endPoint);

            WriteShort(20);
            for (int i = 0; i < 20; ++i)
            {
                WriteIPEndPoint(this.systemEndPoints[i]);
            }

            WriteLong(this.sendPing);
            WriteLong(this.sendPong);
        }
    }
}
