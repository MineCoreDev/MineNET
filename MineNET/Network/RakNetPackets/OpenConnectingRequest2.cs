using System.Net;

namespace MineNET.Network.RakNetPackets
{
    public class OpenConnectingRequest2 : OfflineMessage
    {
        public override byte MessageID { get; } = RakNetProtocol.OpenConnectingRequest2;

        public IPEndPoint EndPoint { get; set; }
        public short MTUSize { get; set; }
        public long ClientID { get; set; }

        public override void Decode()
        {
            base.Decode();

            ReadMagic();
            this.EndPoint = ReadIPEndPoint();
            this.MTUSize = ReadShort();
            this.ClientID = ReadLong();
        }
    }
}
