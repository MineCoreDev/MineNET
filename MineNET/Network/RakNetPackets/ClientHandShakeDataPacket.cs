using System.Collections.Generic;
using System.Net;

namespace MineNET.Network.RakNetPackets
{
    public class ClientHandShakeDataPacket : RakNetPacket
    {
        public override byte MessageID { get; } = RakNetProtocol.ClientHandShakeDataPacket;

        public IPEndPoint EndPoint { get; set; }
        public IPEndPoint[] SystemEndPoints { get; set; }
        public long SendPing { get; set; }
        public long SendPong { get; set; }

        public override void Decode()
        {
            base.Decode();

            List<IPEndPoint> list = new List<IPEndPoint>();

            this.EndPoint = this.ReadIPEndPoint();
            for (int i = 0; i < 20; ++i)
            {
                list.Add(this.ReadIPEndPoint());
            }

            this.SystemEndPoints = list.ToArray();

            this.SendPing = ReadLong();
            this.SendPong = ReadLong();
        }
    }
}
