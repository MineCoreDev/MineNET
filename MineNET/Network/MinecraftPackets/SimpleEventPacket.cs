namespace MineNET.Network.MinecraftPackets
{
    public class SimpleEventPacket : MinecraftPacket
    {
        public const ushort TYPE_ENABLE_COMMANDS = 1;
        public const ushort TYPE_DISABLE_COMMANDS = 2;

        public override byte PacketID { get; } = MinecraftProtocol.SIMPLE_EVENT_PACKET;

        public ushort EventType { get; set; }

        protected override void EncodePayload()
        {
            this.WriteLShort(this.EventType);
        }

        protected override void DecodePayload()
        {
            this.EventType = this.ReadLShort();
        }
    }
}
