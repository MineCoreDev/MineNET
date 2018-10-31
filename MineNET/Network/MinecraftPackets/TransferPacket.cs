namespace MineNET.Network.MinecraftPackets
{
    public class TransferPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.TRANSFER_PACKET;

        public string Address { get; set; }
        public ushort Port { get; set; } = 19132;

        protected override void EncodePayload()
        {
            this.WriteString(this.Address);
            this.WriteLShort(this.Port);
        }

        protected override void DecodePayload()
        {

        }
    }
}
