namespace MineNET.RakNet.Packets
{
    public class DataPacket_1 : DataPacket
    {
        public const int ID = 0x81;

        public override byte PacketID
        {
            get
            {
                return ID;
            }
        }
    }
}
