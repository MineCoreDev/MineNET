namespace MineNET.Network.MinecraftPackets
{
    public class ShowStoreOfferPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.SHOW_STORE_OFFER_PACKET;

        public string OfferId { get; set; }
        public bool ShowAll { get; set; }

        protected override void EncodePayload()
        {
            this.WriteString(this.OfferId);
            this.WriteBool(this.ShowAll);
        }

        protected override void DecodePayload()
        {
            this.OfferId = this.ReadString();
            this.ShowAll = this.ReadBool();
        }
    }
}
