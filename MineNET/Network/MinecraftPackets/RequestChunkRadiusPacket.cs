namespace MineNET.Network.MinecraftPackets
{
    public class RequestChunkRadiusPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.REQUEST_CHUNK_RADIUS_PACKET;

        public int Radius { get; set; }

        protected override void EncodePayload()
        {

        }

        protected override void DecodePayload()
        {
            this.Radius = ReadSVarInt();
        }
    }
}
