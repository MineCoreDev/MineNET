using MineNET.Values;

namespace MineNET.Network.Packets
{
    public class LevelSoundEventPacket : DataPacket
    {
        public const int ID = ProtocolInfo.LEVEL_SOUND_EVENT_PACKET;

        public override byte PacketID
        {
            get
            {
                return LevelSoundEventPacket.ID;
            }
        }

        public byte Sound { get; set; }
        public Vector3 Vector3 { get; set; }
        public int ExtraData { get; set; } = -1;
        public int Pitch { get; set; } = 1;
        public bool IsBabyMob { get; set; }
        public bool IsGlobal { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteByte(this.Sound);
            this.WriteVector3(this.Vector3);
            this.WriteSVarInt(this.ExtraData);
            this.WriteSVarInt(this.Pitch);
            this.WriteBool(this.IsBabyMob);
            this.WriteBool(this.IsGlobal);
        }

        public override void Decode()
        {
            base.Decode();

            this.Sound = this.ReadByte();
            this.Vector3 = this.ReadVector3();
            this.ExtraData = this.ReadSVarInt();
            this.Pitch = this.ReadSVarInt();
            this.IsBabyMob = this.ReadBool();
            this.IsGlobal = this.ReadBool();
        }
    }
}
