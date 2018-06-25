namespace MineNET.Network.RakNetPackets
{
    public class Nack : AcknowledgePacket
    {
        public override byte MessageID { get; } = RakNetProtocol.NackPacket;
    }
}
