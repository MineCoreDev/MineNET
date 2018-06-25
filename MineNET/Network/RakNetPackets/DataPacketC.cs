namespace MineNET.Network.RakNetPackets
{
    public class DataPacketC : DataPacket
    {
        public override byte MessageID { get; } = RakNetProtocol.DataPacketC;
    }
}
