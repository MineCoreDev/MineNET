namespace MineNET.Network.MinecraftPackets
{
    public class PhotoTransferPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.PHOTO_TRANSFER_PACKET;

        public string PhotoName { get; set; }
        public string PhotoData { get; set; }
        public string BookId { get; set; }

        protected override void EncodePayload()
        {
            this.WriteString(this.PhotoName);
            this.WriteString(this.PhotoData);
            this.WriteString(this.BookId);
        }

        protected override void DecodePayload()
        {
            this.PhotoName = this.ReadString();
            this.PhotoData = this.ReadString();
            this.BookId = this.ReadString();
        }
    }
}
