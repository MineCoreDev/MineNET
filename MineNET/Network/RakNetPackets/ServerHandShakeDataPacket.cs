using System.Net;

namespace MineNET.Network.RakNetPackets
{
    public class ServerHandShakeDataPacket : RakNetPacket
    {
        public override byte MessageID { get; } = RakNetProtocol.ServerHandShakeDataPacket;

        public IPEndPoint EndPoint { get; set; }
        public IPEndPoint[] SystemEndPoints { get; set; } = new IPEndPoint[]
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
        public long SendPing { get; set; }
        public long SendPong { get; set; }

        public override void Encode()
        {
            base.Encode();

            WriteIPEndPoint(this.EndPoint);

            WriteShort(20);
            for (int i = 0; i < 20; ++i)
            {
                WriteIPEndPoint(this.SystemEndPoints[i]);
            }

            WriteLong(this.SendPing);
            WriteLong(this.SendPong);
        }
    }
}
