namespace MineNET.Network.MinecraftPackets
{
    public class FullChunkDataPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.FULL_CHUNK_DATA_PACKET;

        public override int OrderChannel => CHANNEL_CHUNK;

        public int ChunkX { get; set; }
        public int ChunkY { get; set; }

        public byte[] Data { get; set; }

        protected override void EncodePayload()
        {
            this.WriteSVarInt(this.ChunkX);
            this.WriteSVarInt(this.ChunkY);

            this.WriteUVarInt((uint) this.Data.Length);
            this.WriteBytes(this.Data);
        }

        protected override void DecodePayload()
        {

        }
    }
}
