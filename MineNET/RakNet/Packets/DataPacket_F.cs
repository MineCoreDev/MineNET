namespace MineNET.RakNet.Packets
{
    public class DataPacket_F : DataPacket
    {
        public const int ID = 0x8F;

        public override byte PacketID
        {
            get
            {
                return ID;
            }
        }
    }
}
