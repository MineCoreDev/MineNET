namespace MineNET.Network.MinecraftPackets
{
    public class ChunkRadiusUpdatedPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.CHUNK_RADIUS_UPDATED_PACKET;

        public int Radius { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteSVarInt(this.Radius);
        }
    }
}
