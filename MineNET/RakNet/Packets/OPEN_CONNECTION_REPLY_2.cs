using System.Net;

namespace MineNET.RakNet.Packets
{
    public class OPEN_CONNECTION_REPLY_2 : OfflineMessage
    {
        public const byte ID = 0x08;

        public override byte PacketID
        {
            get
            {
                return ID;
            }
        }

        long serverID;
        public long ServerID
        {
            get
            {
                return this.serverID;
            }

            set
            {
                this.serverID = value;
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

        short mtuSize;
        public short MTUSize
        {
            get
            {
                return this.mtuSize;
            }

            set
            {
                this.mtuSize = value;
            }
        }

        public override void Encode()
        {
            base.Encode();

            WriteMagic();
            WriteLLong((ulong) this.serverID);
            WriteIPEndPoint(this.endPoint);
            WriteLShort((ushort) this.mtuSize);
            WriteByte(0);
        }
    }
}
