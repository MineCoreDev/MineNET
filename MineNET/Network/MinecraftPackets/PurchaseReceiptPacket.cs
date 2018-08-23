namespace MineNET.Network.MinecraftPackets
{
    public class PurchaseReceiptPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.PURCHASE_RECEIPT_PACKET;

        public string[] Entries { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteUVarInt((uint) this.Entries.Length);
            for (int i = 0; i < this.Entries.Length; ++i)
            {
                this.WriteString(this.Entries[i]);
            }
        }

        public override void Decode()
        {
            base.Decode();

            this.Entries = new string[this.ReadUVarInt()];
            for (int i = 0; i < this.Entries.Length; ++i)
            {
                this.Entries[i] = this.ReadString();
            }
        }
    }
}
