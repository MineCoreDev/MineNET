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
                return serverID;
            }

            set
            {
                serverID = value;
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

        short mtuSize;
        public short MTUSize
        {
            get
            {
                return mtuSize;
            }

            set
            {
                mtuSize = value;
            }
        }

        public override void Encode()
        {
            base.Encode();

            WriteMagic();
            WriteLong(serverID);
            WriteIPEndPoint(endPoint);
            WriteShort(mtuSize);
            WriteByte(0);
        }
    }
}
