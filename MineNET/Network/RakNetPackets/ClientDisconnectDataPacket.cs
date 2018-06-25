namespace MineNET.Network.RakNetPackets
{
    public class ClientDisconnectDataPacket : RakNetPacket
    {
        public override byte MessageID { get; } = RakNetProtocol.ClientDisconnectDataPacket;
    }
}
