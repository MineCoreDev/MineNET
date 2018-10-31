namespace MineNET.Network.MinecraftPackets
{
    public class ClientToServerHandshakePacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.CLIENTBOUND_MAP_ITEM_DATA_PACKET;

        protected override void EncodePayload()
        {

        }

        protected override void DecodePayload()
        {

        }
    }
}
