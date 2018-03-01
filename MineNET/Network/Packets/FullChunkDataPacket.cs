namespace MineNET.Network.Packets
{
    public class FullChunkDataPacket : DataPacket
    {
        public const int ID = ProtocolInfo.FULL_CHUNK_DATA_PACKET;

        public override byte PacketID
        {
            get
            {
                return FullChunkDataPacket.ID;
            }
        }

        public int ChunkX { get; set; }

        public int ChunkY { get; set; }

        public byte[] Data { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteSVarInt(this.ChunkX);
            this.WriteSVarInt(this.ChunkY);

            this.WriteBytes(this.Data);
        }
    }
}
