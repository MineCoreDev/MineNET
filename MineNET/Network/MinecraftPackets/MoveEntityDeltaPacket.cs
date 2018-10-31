namespace MineNET.Network.MinecraftPackets
{
    public class MoveEntityDeltaPacket : MinecraftPacket
    {
        public const byte FLAG_HAS_X = 0x01;
        public const byte FLAG_HAS_Y = 0x02;
        public const byte FLAG_HAS_Z = 0x04;
        public const byte FLAG_HAS_ROT_X = 0x08;
        public const byte FLAG_HAS_ROT_Y = 0x10;
        public const byte FLAG_HAS_ROT_Z = 0x20;

        public override byte PacketID { get; } = MinecraftProtocol.MOVE_ENTITY_DELTA_PACKET;

        public byte Flags { get; set; }
        public int XDiff { get; set; } = 0;
        public int YDiff { get; set; } = 0;
        public int ZDiff { get; set; } = 0;
        public float XRot { get; set; } = 0;
        public float YRot { get; set; } = 0;
        public float ZRot { get; set; } = 0;

        protected override void EncodePayload()
        {
            this.WriteByte(this.Flags);
            this.MaybeWriteCoord(MoveEntityDeltaPacket.FLAG_HAS_X, this.XDiff);
            this.MaybeWriteCoord(MoveEntityDeltaPacket.FLAG_HAS_Y, this.YDiff);
            this.MaybeWriteCoord(MoveEntityDeltaPacket.FLAG_HAS_Z, this.ZDiff);
            this.MaybeWriteRotation(MoveEntityDeltaPacket.FLAG_HAS_ROT_X, this.XRot);
            this.MaybeWriteRotation(MoveEntityDeltaPacket.FLAG_HAS_ROT_Y, this.YRot);
            this.MaybeWriteRotation(MoveEntityDeltaPacket.FLAG_HAS_ROT_Z, this.ZRot);
        }

        protected override void DecodePayload()
        {
            this.Flags = this.ReadByte();
            this.XDiff = this.MaybeReadCoord(MoveEntityDeltaPacket.FLAG_HAS_X);
            this.YDiff = this.MaybeReadCoord(MoveEntityDeltaPacket.FLAG_HAS_Y);
            this.ZDiff = this.MaybeReadCoord(MoveEntityDeltaPacket.FLAG_HAS_Z);
            this.XRot = this.MaybeReadRotation(MoveEntityDeltaPacket.FLAG_HAS_ROT_X);
            this.YRot = this.MaybeReadRotation(MoveEntityDeltaPacket.FLAG_HAS_ROT_Y);
            this.ZRot = this.MaybeReadRotation(MoveEntityDeltaPacket.FLAG_HAS_ROT_Z);
        }

        private void MaybeWriteCoord(byte flag, int val)
        {
            if ((this.Flags & flag) != 0)
            {
                this.WriteVarInt(val);
            }
        }

        private void MaybeWriteRotation(byte flag, float val)
        {
            if ((this.Flags & flag) != 0)
            {
                this.WriteByteRotation(val);
            }
        }

        private int MaybeReadCoord(byte flag)
        {
            if ((this.Flags & flag) != 0)
            {
                return this.ReadVarInt();
            }
            return 0;
        }

        private float MaybeReadRotation(byte flag)
        {

            if ((this.Flags & flag) != 0)
            {
                return this.ReadByteRotation();
            }
            return 0;
        }
    }
}
