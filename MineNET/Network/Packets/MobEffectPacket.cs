namespace MineNET.Network.Packets
{
    public class MobEffectPacket : DataPacket
    {
        public const byte EVENT_ADD = 1;
        public const byte EVENT_MODIFY = 2;
        public const byte EVENT_REMOVE = 3;

        public const int ID = ProtocolInfo.MOB_EFFECT_PACKET;

        public override byte PacketID
        {
            get
            {
                return MobEffectPacket.ID;
            }
        }

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
            this.WriteVarInt(this.EffectId);
            this.WriteVarInt(this.Amplifier);
            this.WriteBool(this.Particles);
            this.WriteVarInt(this.Duration);
        }
    }
}
