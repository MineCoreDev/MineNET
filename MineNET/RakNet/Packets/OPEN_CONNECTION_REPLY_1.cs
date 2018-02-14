namespace MineNET.RakNet.Packets
{
    public class OPEN_CONNECTION_REPLY_1 : OfflineMessage
    {
        public const byte ID = 0x06;

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

            this.WriteMagic();
            this.WriteLong(serverID);
            this.WriteByte(0);
            this.WriteShort(mtuSize);
        }
    }
}
