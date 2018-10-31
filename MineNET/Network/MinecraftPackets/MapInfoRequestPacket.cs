namespace MineNET.Network.MinecraftPackets
{
    public class MapInfoRequestPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.MAP_INFO_REQUEST_PACKET;

        public long MapId { get; set; }

        protected override void EncodePayload()
        {

        }

        protected override void DecodePayload()
        {
            this.MapId = this.ReadEntityUniqueId();
        }
    }
}
