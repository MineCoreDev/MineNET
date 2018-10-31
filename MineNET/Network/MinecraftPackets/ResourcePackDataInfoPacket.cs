namespace MineNET.Network.MinecraftPackets
{
    public class ResourcePackDataInfoPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.RESOURCE_PACK_DATA_INFO_PACKET;

        public string PackId { get; set; }
        public uint MaxChunkSize { get; set; }
        public uint ChunkCount { get; set; }
        public ulong CompressedPackSize { get; set; }
        public byte[] Sha256 { get; set; }

        protected override void EncodePayload()
        {
            base.Encode();

            this.WriteString(this.PackId);
            this.WriteLInt(this.MaxChunkSize);
            this.WriteLInt(this.ChunkCount);
            this.WriteLLong(this.CompressedPackSize);
            this.WriteBytes(this.Sha256);
        }

        protected override void DecodePayload()
        {

        }
    }
}
