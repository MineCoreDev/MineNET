namespace MineNET.Network.RakNetPackets
{
    public class DataPacket1 : DataPacket
    {
        public override byte MessageID { get; } = RakNetProtocol.DataPacket1;
    }
}
