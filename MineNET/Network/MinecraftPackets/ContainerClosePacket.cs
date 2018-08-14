namespace MineNET.Network.MinecraftPackets
{
    public class ContainerClosePacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.CONTAINER_CLOSE_PACKET;

        public byte WindowId { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteByte(this.WindowId);
        }

        public override void Decode()
        {
            base.Decode();

            this.WindowId = this.ReadByte();
        }
    }
}
