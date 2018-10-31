namespace MineNET.Network.MinecraftPackets
{
    public class AnimatePacket : MinecraftPacket
    {
        public const int ACTION_NO_ACTION = 0;
        public const int ACTION_SWING = 1;
        public const int ACTION_WAKE_UP = 3;
        public const int ACTION_CRITICAL_HIT = 4;
        public const int ACTION_MAGIC_CRITICAL_HIT = 5;
        public const int ACTION_ROW_RIGHT = 128;
        public const int ACTION_ROW_LEFT = 129;

        public override byte PacketID { get; } = MinecraftProtocol.ANIMATE_PACKET;

        public int Action { get; set; }
        public long EntityRuntimeId { get; set; }
        public float Unknown { get; set; }

        protected override void EncodePayload()
        {
            this.WriteSVarInt(this.Action);
            this.WriteEntityRuntimeId(this.EntityRuntimeId);
            if ((this.Action & 0x80) != 0)
            {
                this.WriteLFloat(this.Unknown);
            }
        }

        protected override void DecodePayload()
        {
            this.Action = this.ReadSVarInt();
            this.EntityRuntimeId = this.ReadEntityRuntimeId();
            if ((this.Action & 0x80) != 0)
            {
                this.Unknown = this.ReadLFloat();
            }
        }
    }
}
