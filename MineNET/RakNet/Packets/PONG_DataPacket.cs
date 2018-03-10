namespace MineNET.RakNet.Packets
{
    public class PONG_DataPacket : Packet
    {
        public const int ID = 0x03;

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

            WriteLLong((ulong) this.pingID);
            WriteLLong((ulong) this.pingID);
        }
    }
}
