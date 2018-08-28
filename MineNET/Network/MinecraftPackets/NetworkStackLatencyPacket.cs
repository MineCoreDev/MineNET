namespace MineNET.Network.MinecraftPackets
{
    public class NetworkStackLatencyPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.NETWORK_STACK_LATENCY_PACKET;

        public ulong Timestamp { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteLLong(this.Timestamp);
        }

        public override void Decode()
        {
            base.Decode();

            this.Timestamp = this.ReadLLong();
        }
    }
}
