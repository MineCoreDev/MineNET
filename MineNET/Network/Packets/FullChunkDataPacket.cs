namespace MineNET.Network.Packets
{
    public class FullChunkDataPacket : DataPacket
    {
        public const int ID = ProtocolInfo.FULL_CHUNK_DATA_PACKET;

        public override byte PacketID
        {
            get
            {
                return ID;
            }
        }

        int chunkX;
        public int ChunkX
        {
            get
            {
                return this.chunkX;
            }

            set
            {
                this.chunkX = value;
            }
        }

        int chunkY;
        public int ChunkY
        {
            get
            {
                return this.chunkY;
            }

            set
            {
                this.chunkY = value;
            }
        }

        string data;
        public string Data
        {
            get
            {
                return data;
            }

            set
            {
                this.data = value;
            }
        }
    }
}
