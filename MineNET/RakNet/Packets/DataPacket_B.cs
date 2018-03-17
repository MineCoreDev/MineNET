namespace MineNET.RakNet.Packets
{
    public class DataPacket_B : DataPacket
    {
        public const int ID = 0x8B;

        public override byte PacketID
        {
            get
            {
                return ID;
            }
        }
    }
}
