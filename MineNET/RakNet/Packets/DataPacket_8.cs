namespace MineNET.RakNet.Packets
{
    public class DataPacket_8 : DataPacket
    {
        public const int ID = 0x88;

        public override byte PacketID
        {
            get
            {
                return ID;
            }
        }
    }
}
