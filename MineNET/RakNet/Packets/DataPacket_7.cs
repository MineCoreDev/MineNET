namespace MineNET.RakNet.Packets
{
    public class DataPacket_7 : DataPacket
    {
        public const int ID = 0x87;

        public override byte PacketID
        {
            get
            {
                return ID;
            }
        }
    }
}
