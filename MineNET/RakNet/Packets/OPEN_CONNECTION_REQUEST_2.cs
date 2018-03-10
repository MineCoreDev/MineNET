using System.Net;

namespace MineNET.RakNet.Packets
{
    public class OPEN_CONNECTION_REQUEST_2 : OfflineMessage
    {
        public const byte ID = 0x07;

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
        }

        short mtuSize;
        public short MTUSize
        {
            get
            {
                return this.mtuSize;
            }
        }

        long clientID;
        public long ClientID
        {
            get
            {
                return this.clientID;
            }
        }

        public override void Decode()
        {
            base.Decode();

            ReadMagic();
            this.endPoint = ReadIPEndPoint();
            this.mtuSize = (short) ReadLShort();
            this.clientID = (long) ReadLLong();
        }
    }
}
