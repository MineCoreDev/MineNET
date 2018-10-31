namespace MineNET.Network.MinecraftPackets
{
    public class ShowCreditsPacket : MinecraftPacket
    {
        public const int STATUS_START_CREDITS = 0;
        public const int STATUS_END_CREDITS = 1;

        public override byte PacketID { get; } = MinecraftProtocol.SHOW_CREDITS_PACKET;

        public long EntityRuntimeId { get; set; }
        public int Status { get; set; }

        protected override void EncodePayload()
        {
            this.WriteEntityRuntimeId(this.EntityRuntimeId);
            this.WriteVarInt(this.Status);
        }

        protected override void DecodePayload()
        {

        }
    }
}
