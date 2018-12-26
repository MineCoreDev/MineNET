using MineNET.Values;

namespace MineNET.Network.MinecraftPackets
{
    public class LevelSoundEventPacketV1 : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.LEVEL_SOUND_EVENT_PACKET_V1;

        public byte Sound { get; set; }
        public Vector3 Position { get; set; }
        public int ExtraData { get; set; }
        public int EntityType { get; set; } = 1;
        public bool IsBabyMob { get; set; } = false;
        public bool DisableRelativeVolume { get; set; } = false;

        protected override void EncodePayload()
        {
            this.WriteByte(this.Sound);
            this.WriteVector3(this.Position);
            this.WriteVarInt(this.ExtraData);
            this.WriteVarInt(this.EntityType);
            this.WriteBool(this.IsBabyMob);
            this.WriteBool(this.DisableRelativeVolume);
        }

        protected override void DecodePayload()
        {
            this.Sound = this.ReadByte();
            this.Position = this.ReadVector3();
            this.ExtraData = this.ReadVarInt();
            this.EntityType = this.ReadVarInt();
            this.IsBabyMob = this.ReadBool();
            this.DisableRelativeVolume = this.ReadBool();
        }
    }
}
