namespace MineNET.RakNet.Packets
{
    public class DataPacket_9 : DataPacket
    {
        public const int ID = 0x89;

        public override byte PacketID
        {
            get
            {
                return ID;
            }
        }
    }
}
