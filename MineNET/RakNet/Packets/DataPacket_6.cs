namespace MineNET.RakNet.Packets
{
    public class DataPacket_6 : DataPacket
    {
        public const int ID = 0x86;

        public override byte PacketID
        {
            get
            {
                return ID;
            }
        }
    }
}
