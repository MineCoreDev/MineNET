namespace MineNET.Network.MinecraftPackets
{
    public class RequestChunkRadiusPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.REQUEST_CHUNK_RADIUS_PACKET;

        public int Radius { get; set; }

        public override void Decode()
        {
            base.Decode();

            this.Radius = ReadSVarInt();
        }
    }
}
