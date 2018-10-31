namespace MineNET.Network.MinecraftPackets
{
    public class ChunkRadiusUpdatedPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.CHUNK_RADIUS_UPDATED_PACKET;

        public int Radius { get; set; }

        protected override void EncodePayload()
        {
            this.WriteSVarInt(this.Radius);
        }

        protected override void DecodePayload()
        {

        }
    }
}
