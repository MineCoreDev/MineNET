namespace MineNET.RakNet.Packets
{
    public class DataPacket_2 : DataPacket
    {
        public const int ID = 0x82;

        public override byte PacketID
        {
            get
            {
                return ID;
            }
        }
    }
}
