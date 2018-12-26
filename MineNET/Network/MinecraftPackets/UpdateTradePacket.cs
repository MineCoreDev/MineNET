namespace MineNET.Network.MinecraftPackets
{
    public class UpdateTradePacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.UPDATE_TRADE_PACKET;

        public byte WindowId { get; set; }
        public byte WindowType { get; set; }
        public int VarInt1 { get; set; }
        public int VarInt2 { get; set; }
        public int VarInt3 { get; set; }
        public bool IsWilling { get; set; }
        public long TraderUniqueId { get; set; }
        public long PlayerUniqueId { get; set; }
        public string DisplayName { get; set; }
        public byte[] Offers { get; set; }

        protected override void EncodePayload()
        {
            this.WriteByte(this.WindowId);
            this.WriteByte(this.WindowType);
            this.WriteVarInt(this.VarInt1);
            this.WriteVarInt(this.VarInt2);
            this.WriteVarInt(this.VarInt3);
            this.WriteBool(this.IsWilling);
            this.WriteEntityUniqueId(this.TraderUniqueId);
            this.WriteEntityUniqueId(this.PlayerUniqueId);
            this.WriteString(this.DisplayName);
            this.WriteBytes(this.Offers);
        }

        protected override void DecodePayload()
        {

        }
    }
}
