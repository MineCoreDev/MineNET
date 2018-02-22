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
                return ID;
            }
        }

        public long entityRuntimeId;
        public long EntityRuntimeId
        {
            get
            {
                return this.entityRuntimeId;
            }

            set
            {
                this.entityRuntimeId = value;
            }
        }

        Vector3 pos;
        public Vector3 Pos
        {
            get
            {
                return this.pos;
            }

            set
            {
                this.pos = value;
            }
        }

        Vector3 yawPitchHead;
        public Vector3 YawPitchHead
        {
            get
            {
                return this.yawPitchHead;
            }

            set
            {
                this.yawPitchHead = value;
            }
        }

        byte mode;
        public byte Mode
        {
            get
            {
                return this.mode;
            }

            set
            {
                this.mode = value;
            }
        }

        bool onGround;
        public bool OnGround
        {
            get
            {
                return onGround;
            }

            set
            {
                onGround = true;
            }
        }

        long otherEntityRuntimeId;
        public long OtherEntityRuntimeId
        {
            get
            {
                return otherEntityRuntimeId;
            }

            set
            {
                otherEntityRuntimeId = value;
            }
        }

        public override void Encode()
        {
            base.Encode();

            this.WriteSVarLong(this.entityRuntimeId);
            this.WriteVector3(this.pos);
            this.WriteVector3(this.yawPitchHead);
            this.WriteByte(this.mode);
            this.WriteBool(this.onGround);
            this.WriteSVarLong(this.otherEntityRuntimeId);
        }

        public override void Decode()
        {
            base.Decode();

            this.entityRuntimeId = this.ReadSVarLong();
            this.pos = this.ReadVector3();
            this.yawPitchHead = this.ReadVector3();
            this.mode = this.ReadByte();
            this.onGround = this.ReadBool();
            this.otherEntityRuntimeId = this.ReadSVarLong();
        }
    }
}
