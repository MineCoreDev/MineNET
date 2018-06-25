namespace MineNET.Network.RakNetPackets
{
    public class DataPacket2 : DataPacket
    {
        public override byte MessageID { get; } = RakNetProtocol.DataPacket2;
    }
}
