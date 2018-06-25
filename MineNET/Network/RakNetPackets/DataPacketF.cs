namespace MineNET.Network.RakNetPackets
{
    public class DataPacketF : DataPacket
    {
        public override byte MessageID { get; } = RakNetProtocol.DataPacketF;
    }
}
