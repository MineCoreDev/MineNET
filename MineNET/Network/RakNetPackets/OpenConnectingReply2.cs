using System.Net;

namespace MineNET.Network.RakNetPackets
{
    public class OpenConnectingReply2 : OfflineMessage
    {
        public override byte MessageID { get; } = RakNetProtocol.OpenConnectingReply2;

        public long ServerID { get; set; }
        public IPEndPoint EndPoint { get; set; }
        public short MTUSize { get; set; }

        public override void Encode()
        {
            base.Encode();

            WriteMagic();
            WriteLong(this.ServerID);
            WriteIPEndPoint(this.EndPoint);
            WriteShort(this.MTUSize);
            WriteByte(0);
        }
    }
}
