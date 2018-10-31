namespace MineNET.Network.MinecraftPackets
{
    public class UpdateSoftEnumPacket : MinecraftPacket
    {
        public const byte TYPE_ADD = 0;
        public const byte TYPE_REMOVE = 1;
        public const byte TYPE_SET = 2;

        public override byte PacketID { get; } = MinecraftProtocol.UPDATE_SOFT_ENUM_PACKET;

        public string EnumName { get; set; }
        public string[] Values { get; set; }
        public byte Type { get; set; }

        protected override void EncodePayload()
        {
            this.WriteString(this.EnumName);
            this.WriteUVarInt((uint) this.Values.Length);
            for (int i = 0; i < this.Values.Length; ++i)
            {
                this.WriteString(this.Values[i]);
            }
            this.WriteByte(this.Type);
        }

        protected override void DecodePayload()
        {
            this.EnumName = this.ReadString();
            this.Values = new string[this.ReadUVarInt()];
            for (int i = 0; i < this.Values.Length; ++i)
            {
                this.Values[i] = this.ReadString();
            }
            this.Type = this.ReadByte();
        }
    }
}
