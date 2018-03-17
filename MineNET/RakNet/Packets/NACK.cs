namespace MineNET.RakNet.Packets
{
    public class NACK : AcknowledgePacket
    {
        public const int ID = 0xA0;

        public override byte PacketID
        {
            get
            {
                return ID;
            }
        }
    }
}
