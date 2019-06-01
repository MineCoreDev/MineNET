namespace MineNET.Network.MinecraftPackets
{
    public class VideoStreamConnectPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.VIDEO_STREAM_CONNECT_PACKET;

        protected override void DecodePayload()
        {
        }

        protected override void EncodePayload()
        {
        }
    }
}
