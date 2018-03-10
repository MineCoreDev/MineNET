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
                return this.serverID;
            }

            set
            {
                this.serverID = value;
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

            this.WriteMagic();
            this.WriteLLong((ulong) this.serverID);
            this.WriteByte(0);
            this.WriteLShort((ushort) this.mtuSize);
        }
    }
}
