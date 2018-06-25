namespace MineNET.Network.RakNetPackets
{
    public class Ack : AcknowledgePacket
    {
        public override byte MessageID { get; } = RakNetProtocol.AckPacket;
    }
}
