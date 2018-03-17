namespace MineNET.RakNet.Packets
{
    public class DataPacket_5 : DataPacket
    {
        public const int ID = 0x85;

        public override byte PacketID
        {
            get
            {
                return ID;
            }
        }
    }
}
