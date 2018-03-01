using MineNET.Values;

namespace MineNET.Network.Packets
{
    public class MovePlayerPacket : DataPacket
    {
        public const int ID = ProtocolInfo.MOVE_PLAYER_PACKET;

        public const int MODE_NORMAL = 0;
        public const int MODE_RESET = 1;
        public const int MODE_TELEPORT = 2;
        public const int MODE_PITCH = 3; //facepalm Mojang

        public override byte PacketID
        {
            get
            {
                return MovePlayerPacket.ID;
            }
        }

        public long EntityRuntimeId { get; set; }

        public Vector3 Pos { get; set; }

        public Vector3 YawPitchHead { get; set; }

        public byte Mode { get; set; }

        public bool OnGround { get; set; }

        public long OtherEntityRuntimeId { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteSVarLong(this.EntityRuntimeId);
            this.WriteVector3(this.Pos);
            this.WriteVector3(this.YawPitchHead);
            this.WriteByte(this.Mode);
            this.WriteBool(this.OnGround);
            this.WriteSVarLong(this.OtherEntityRuntimeId);
        }

        public override void Decode()
        {
            base.Decode();

            this.EntityRuntimeId = this.ReadSVarLong();
            this.Pos = this.ReadVector3();
            this.YawPitchHead = this.ReadVector3();
            this.Mode = this.ReadByte();
            this.OnGround = this.ReadBool();
            this.OtherEntityRuntimeId = this.ReadSVarLong();
        }
    }
}
