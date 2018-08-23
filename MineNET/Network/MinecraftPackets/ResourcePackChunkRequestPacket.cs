namespace MineNET.Network.MinecraftPackets
{
    public class ResourcePackChunkRequestPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.RESOURCE_PACK_CHUNK_REQUEST_PACKET;

        public string PackId { get; set; }
        public uint ChunkIndex { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteString(this.PackId);
            this.WriteLInt(this.ChunkIndex);
        }

        public override void Decode()
        {
            base.Decode();

            this.PackId = this.ReadString();
            this.ChunkIndex = this.ReadLInt();
        }
    }
}
