namespace MineNET.Network.MinecraftPackets
{
    public class TransferPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.TRANSFER_PACKET;

        public string Address { get; set; }
        public ushort Port { get; set; } = 19132;

        public override void Encode()
        {
            base.Encode();

            this.WriteString(this.Address);
            this.WriteLShort(this.Port);
        }
    }
}
