namespace MineNET.RakNet.Packets
{
    public class DataPacket_C : DataPacket
    {
        public const int ID = 0x8C;

        public override byte PacketID
        {
            get
            {
                return ID;
            }
        }
    }
}
