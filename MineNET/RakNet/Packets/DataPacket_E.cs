namespace MineNET.RakNet.Packets
{
    public class DataPacket_E : DataPacket
    {
        public const int ID = 0x8E;

        public override byte PacketID
        {
            get
            {
                return ID;
            }
        }
    }
}
