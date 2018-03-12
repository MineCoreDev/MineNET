namespace MineNET.RakNet.Packets
{
    public class OPEN_CONNECTION_REQUEST_1 : OfflineMessage
    {
        public const byte ID = 0x05;

        public override byte PacketID
        {
            get
            {
                return ID;
            }
        }

        byte protocol;
        public byte Protocol
        {
            get
            {
                return this.protocol;
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

        public override void Decode()
        {
            base.Decode();

            this.ReadMagic();
            this.protocol = this.ReadByte();
            this.mtuSize = (short) (this.ReadBytes().Length + 18);
        }
    }
}
