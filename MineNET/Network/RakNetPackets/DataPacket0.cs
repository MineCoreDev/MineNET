namespace MineNET.Network.RakNetPackets
{
    public class DataPacket0 : DataPacket
    {
        public override byte MessageID { get; } = RakNetProtocol.DataPacket0;
    }
}
