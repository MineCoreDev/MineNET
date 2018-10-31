namespace MineNET.Network.MinecraftPackets
{
    public class ResourcePackChunkRequestPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.RESOURCE_PACK_CHUNK_REQUEST_PACKET;

        public string PackId { get; set; }
        public uint ChunkIndex { get; set; }

        protected override void EncodePayload()
        {
            this.WriteString(this.PackId);
            this.WriteLInt(this.ChunkIndex);
        }

        protected override void DecodePayload()
        {
            this.PackId = this.ReadString();
            this.ChunkIndex = this.ReadLInt();
        }
    }
}
