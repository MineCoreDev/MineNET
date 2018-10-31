namespace MineNET.Network.MinecraftPackets
{
    public class SetTitlePacket : MinecraftPacket
    {
        public const int TYPE_CLEAR_TITLE = 0;
        public const int TYPE_RESET_TITLE = 1;
        public const int TYPE_SET_TITLE = 2;
        public const int TYPE_SET_SUBTITLE = 3;
        public const int TYPE_SET_ACTIONBAR_MESSAGE = 4;
        public const int TYPE_SET_ANIMATION_TIMES = 5;

        public override byte PacketID { get; } = MinecraftProtocol.SET_TITLE_PACKET;

        public int Type { get; set; }
        public string Text { get; set; }
        public int FadeInTime { get; set; } = 0;
        public int StayTime { get; set; } = 0;
        public int FadeOutTime { get; set; } = 0;

        protected override void EncodePayload()
        {
            this.WriteVarInt(this.Type);
            this.WriteString(this.Text);
            this.WriteVarInt(this.FadeInTime);
            this.WriteVarInt(this.StayTime);
            this.WriteVarInt(this.FadeOutTime);
        }

        protected override void DecodePayload()
        {

        }
    }
}
