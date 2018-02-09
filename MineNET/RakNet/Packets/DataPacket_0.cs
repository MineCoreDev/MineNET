namespace MineNET.RakNet.Packets
{
    public class DataPacket_0 : DataPacket
    {
        public const int ID = 0x80;

        public override byte PacketID
        {
            get
            {
                return ID;
            }
        }
    }
}
