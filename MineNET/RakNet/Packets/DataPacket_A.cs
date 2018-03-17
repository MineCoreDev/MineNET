namespace MineNET.RakNet.Packets
{
    public class DataPacket_A : DataPacket
    {
        public const int ID = 0x8A;

        public override byte PacketID
        {
            get
            {
                return ID;
            }
        }
    }
}
