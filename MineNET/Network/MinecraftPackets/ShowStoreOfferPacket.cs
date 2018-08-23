namespace MineNET.Network.MinecraftPackets
{
    public class ShowStoreOfferPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.SHOW_STORE_OFFER_PACKET;

        public string OfferId { get; set; }
        public bool ShowAll { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteString(this.OfferId);
            this.WriteBool(this.ShowAll);
        }

        public override void Decode()
        {
            base.Decode();

            this.OfferId = this.ReadString();
            this.ShowAll = this.ReadBool();
        }
    }
}
