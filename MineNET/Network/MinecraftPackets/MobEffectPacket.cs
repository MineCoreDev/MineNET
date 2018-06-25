namespace MineNET.Network.MinecraftPackets
{
    public class MobEffectPacket : MinecraftPacket
    {
        public const byte EVENT_ADD = 1;
        public const byte EVENT_MODIFY = 2;
        public const byte EVENT_REMOVE = 3;

        public override byte PacketID { get; } = MinecraftProtocol.MOB_EFFECT_PACKET;

        public long EntityRuntimeId { get; set; }
        public int EventId { get; set; }
        public int EffectId { get; set; }
        public int Amplifier { get; set; }
        public bool Particles { get; set; }
        public int Duration { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteEntityRuntimeId(this.EntityRuntimeId);
            this.WriteByte((byte) this.EventId);
            this.WriteSVarInt(this.EffectId);
            this.WriteSVarInt(this.Amplifier);
            this.WriteBool(this.Particles);
            this.WriteSVarInt(this.Duration);
        }
    }
}
