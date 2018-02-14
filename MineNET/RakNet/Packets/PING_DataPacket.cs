namespace MineNET.RakNet.Packets
{
    public class PING_DataPacket : Packet
    {
        public const int ID = 0x00;

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
                return pingID;
            }
        }

        public override void Decode()
        {
            base.Decode();

            pingID = ReadLong();
        }
    }
}
