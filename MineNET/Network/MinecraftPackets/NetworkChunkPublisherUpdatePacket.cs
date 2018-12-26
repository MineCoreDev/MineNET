using MineNET.Values;

namespace MineNET.Network.MinecraftPackets
{
    public class NetworkChunkPublisherUpdatePacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.NETWORK_CHUNK_PUBLISHER_UPDATE_PACKET;

        public BlockCoordinate3D Position { get; set; }
        public uint Radius { get; set; }

        protected override void EncodePayload()
        {
            this.WriteSBlockVector3(this.Position);
            this.WriteUVarInt(this.Radius);
        }

        protected override void DecodePayload()
        {
            this.Position = this.ReadSBlockVector3();
            this.Radius = this.ReadUVarInt();
        }
    }
}
