namespace MineNET.Network.MinecraftPackets
{
    public class ResourcePackChunkDataPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.RESOURCE_PACK_CHUNK_DATA_PACKET;

        public string PackId { get; set; }
        public uint ChunkIndex { get; set; }
        public ulong Progress { get; set; }
        public byte[] Data { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteString(this.PackId);
            this.WriteLInt(this.ChunkIndex);
            this.WriteLLong(this.Progress);
            this.WriteLInt((uint) this.Data.Length);
            this.WriteBytes(this.Data);
        }

        public override void Decode()
        {
            base.Decode();

            this.PackId = this.ReadString();
            this.ChunkIndex = this.ReadLInt();
            this.Progress = this.ReadLLong();
            this.Data = this.ReadBytes((int) this.ReadLInt());
        }
    }
}
