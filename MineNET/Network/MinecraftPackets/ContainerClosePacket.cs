namespace MineNET.Network.MinecraftPackets
{
    public class ContainerClosePacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.CONTAINER_CLOSE_PACKET;

        public byte WindowId { get; set; }

        protected override void EncodePayload()
        {
            this.WriteByte(this.WindowId);
        }

        protected override void DecodePayload()
        {
            this.WindowId = this.ReadByte();
        }
    }
}
