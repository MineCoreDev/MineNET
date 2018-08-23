namespace MineNET.Network.MinecraftPackets
{
    public class SimpleEventPacket : MinecraftPacket
    {
        public const ushort TYPE_ENABLE_COMMANDS = 1;
        public const ushort TYPE_DISABLE_COMMANDS = 2;

        public override byte PacketID { get; } = MinecraftProtocol.SIMPLE_EVENT_PACKET;

        public ushort EventType { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteLShort(this.EventType);
        }

        public override void Decode()
        {
            base.Decode();

            this.EventType = this.ReadLShort();
        }
    }
}
