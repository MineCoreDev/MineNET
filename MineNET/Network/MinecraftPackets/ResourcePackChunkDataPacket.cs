namespace MineNET.Network.MinecraftPackets
{
    public class ResourcePackChunkDataPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.RESOURCE_PACK_CHUNK_DATA_PACKET;

        public string PackId { get; set; }
        public uint ChunkIndex { get; set; }
        public ulong Progress { get; set; }
        public byte[] Data { get; set; }

        protected override void EncodePayload()
        {
            this.WriteString(this.PackId);
            this.WriteLInt(this.ChunkIndex);
            this.WriteLLong(this.Progress);
            this.WriteLInt((uint) this.Data.Length);
            this.WriteBytes(this.Data);
        }

        protected override void DecodePayload()
        {
            this.PackId = this.ReadString();
            this.ChunkIndex = this.ReadLInt();
            this.Progress = this.ReadLLong();
            this.Data = this.ReadBytes((int) this.ReadLInt());
        }
    }
}
