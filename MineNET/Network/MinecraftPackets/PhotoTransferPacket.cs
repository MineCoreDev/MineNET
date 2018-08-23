namespace MineNET.Network.MinecraftPackets
{
    public class PhotoTransferPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.PHOTO_TRANSFER_PACKET;

        public string PhotoName { get; set; }
        public string PhotoData { get; set; }
        public string BookId { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteString(this.PhotoName);
            this.WriteString(this.PhotoData);
            this.WriteString(this.BookId);
        }

        public override void Decode()
        {
            base.Decode();

            this.PhotoName = this.ReadString();
            this.PhotoData = this.ReadString();
            this.BookId = this.ReadString();
        }
    }
}
