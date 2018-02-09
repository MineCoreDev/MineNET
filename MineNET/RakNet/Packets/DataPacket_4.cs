namespace MineNET.RakNet.Packets
{
    public class DataPacket_4 : DataPacket
    {
        public const int ID = 0x84;

        public override byte PacketID
        {
            get
            {
                return ID;
            }
        }
    }
}
