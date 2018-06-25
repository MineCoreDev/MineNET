namespace MineNET.Network.RakNetPackets
{
    public class DataPacketD : DataPacket
    {
        public override byte MessageID { get; } = RakNetProtocol.DataPacketD;
    }
}
