namespace MineNET.Network.MinecraftPackets
{
    public class NetworkStackLatencyPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.NETWORK_STACK_LATENCY_PACKET;

        public ulong Timestamp { get; set; }
        public bool UnknownBool { get; set; }

        protected override void EncodePayload()
        {
            this.WriteLLong(this.Timestamp);
            this.WriteBool(this.UnknownBool);
        }

        protected override void DecodePayload()
        {
            this.Timestamp = this.ReadLLong();
            this.UnknownBool = this.ReadBool();
        }
    }
}
