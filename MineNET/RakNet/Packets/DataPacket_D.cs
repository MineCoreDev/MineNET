namespace MineNET.RakNet.Packets
{
    public class DataPacket_D : DataPacket
    {
        public const int ID = 0x8D;

        public override byte PacketID
        {
            get
            {
                return ID;
            }
        }
    }
}
