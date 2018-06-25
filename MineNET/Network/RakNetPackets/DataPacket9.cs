namespace MineNET.Network.RakNetPackets
{
    public class DataPacket9 : DataPacket
    {
        public override byte MessageID { get; } = RakNetProtocol.DataPacket9;
    }
}
