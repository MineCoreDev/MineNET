namespace MineNET.RakNet.Packets
{
    public class UNCONNECTED_PING : OfflineMessage
    {
        public const byte ID = 0x01;

        public override byte PacketID
        {
            get
            {
                return ID;
            }
        }

        long pingID;
        public long PingID
        {
            get
            {
                return this.pingID;
            }

            set
            {
                this.pingID = value;
            }
        }

        public override void Encode()
        {
            base.Encode();
            this.WriteLong(this.pingID);
            this.WriteMagic();
        }

        public override void Decode()
        {
            base.Decode();
            this.pingID = this.ReadLong();
            this.ReadMagic();
        }
    }
}
