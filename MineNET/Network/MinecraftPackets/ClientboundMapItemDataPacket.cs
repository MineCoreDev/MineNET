namespace MineNET.Network.MinecraftPackets
{
    public class ClientboundMapItemDataPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.CLIENTBOUND_MAP_ITEM_DATA_PACKET;
    }
}
