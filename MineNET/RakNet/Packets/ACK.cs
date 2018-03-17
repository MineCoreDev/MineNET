namespace MineNET.RakNet.Packets
{
    public class ACK : AcknowledgePacket
    {
        public const int ID = 0xC0;

        public override byte PacketID
        {
            get
            {
                return ID;
            }
        }
    }
}
