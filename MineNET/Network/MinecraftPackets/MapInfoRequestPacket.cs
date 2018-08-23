namespace MineNET.Network.MinecraftPackets
{
    public class MapInfoRequestPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.MAP_INFO_REQUEST_PACKET;

        public long MapId { get; set; }

        public override void Decode()
        {
            base.Decode();

            this.MapId = this.ReadEntityUniqueId();
        }
    }
}
