namespace MineNET.RakNet.Packets
{
    public class DataPacket_3 : DataPacket
    {
        public const int ID = 0x83;

        public override byte PacketID
        {
            get
            {
                return ID;
            }
        }
    }
}
