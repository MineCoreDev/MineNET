namespace MineNET.Network.MinecraftPackets
{
    public class NetworkStackLatencyPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.NETWORK_STACK_LATENCY_PACKET;

        public ulong Timestamp { get; set; }

        protected override void EncodePayload()
        {
            this.WriteLLong(this.Timestamp);
        }

        protected override void DecodePayload()
        {
            this.Timestamp = this.ReadLLong();
        }
    }
}
