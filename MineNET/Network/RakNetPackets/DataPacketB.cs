namespace MineNET.Network.RakNetPackets
{
    public class DataPacketB : DataPacket
    {
        public override byte MessageID { get; } = RakNetProtocol.DataPacketB;
    }
}
