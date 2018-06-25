namespace MineNET.Network.MinecraftPackets
{
    public class FullChunkDataPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.FULL_CHUNK_DATA_PACKET;

        public int ChunkX { get; set; }
        public int ChunkY { get; set; }

        public byte[] Data { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteSVarInt(this.ChunkX);
            this.WriteSVarInt(this.ChunkY);

            this.WriteUVarInt((uint) this.Data.Length);
            this.WriteBytes(this.Data);
        }

    }
}
