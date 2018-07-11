namespace MineNET.Network.MinecraftPackets
{
    public class BookEditPacket : MinecraftPacket
    {
        public const int TYPE_REPLACE_PAGE = 0;
        public const int TYPE_ADD_PAGE = 1;
        public const int TYPE_DELETE_PAGE = 2;
        public const int TYPE_SWAP_PAGES = 3;
        public const int TYPE_SIGN_BOOK = 4;

        public override byte PacketID { get; } = MinecraftProtocol.BOOK_EDIT_PACKET;

        public byte Type { get; set; }
        public byte InventorySlot { get; set; }
        public byte PageNumber { get; set; }
        public byte SecondaryNumber { get; set; }
        public string Text { get; set; }
        public string PhotoName { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Xuid { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteByte(this.Type);
            this.WriteByte(this.InventorySlot);
            switch (this.Type)
            {
                case BookEditPacket.TYPE_REPLACE_PAGE:
                case BookEditPacket.TYPE_ADD_PAGE:
                    this.WriteByte(this.PageNumber);
                    this.WriteString(this.Text);
                    this.WriteString(this.PhotoName);
                    break;
                case BookEditPacket.TYPE_DELETE_PAGE:
                    this.WriteByte(this.PageNumber);
                    break;
                case BookEditPacket.TYPE_SWAP_PAGES:
                    this.WriteByte(this.PageNumber);
                    this.WriteByte(this.SecondaryNumber);
                    break;
                case BookEditPacket.TYPE_SIGN_BOOK:
                    this.WriteString(this.Title);
                    this.WriteString(this.Author);
                    this.WriteString(this.Xuid);
                    break;
            }
        }

        public override void Decode()
        {
            base.Decode();

            this.Type = this.ReadByte();
            this.InventorySlot = this.ReadByte();

            switch (this.Type)
            {
                case BookEditPacket.TYPE_REPLACE_PAGE:
                case BookEditPacket.TYPE_ADD_PAGE:
                    this.PageNumber = this.ReadByte();
                    this.Text = this.ReadString();
                    this.PhotoName = this.ReadString();
                    break;
                case BookEditPacket.TYPE_DELETE_PAGE:
                    this.PageNumber = this.ReadByte();
                    break;
                case BookEditPacket.TYPE_SWAP_PAGES:
                    this.PageNumber = this.ReadByte();
                    this.SecondaryNumber = this.ReadByte();
                    break;
                case BookEditPacket.TYPE_SIGN_BOOK:
                    this.Title = this.ReadString();
                    this.Author = this.ReadString();
                    this.Xuid = this.ReadString();
                    break;
            }
        }
    }
}
