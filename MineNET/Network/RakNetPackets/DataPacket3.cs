namespace MineNET.Network.RakNetPackets
{
    public class DataPacket3 : DataPacket
    {
        public override byte MessageID { get; } = RakNetProtocol.DataPacket3;
    }
}
