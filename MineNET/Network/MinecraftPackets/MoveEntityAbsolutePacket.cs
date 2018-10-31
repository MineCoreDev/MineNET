using MineNET.Values;

namespace MineNET.Network.MinecraftPackets
{
    public class MoveEntityAbsolutePacket : MinecraftPacket
    {
        public const int FLAG_GROUND = 0x01;
        public const int FLAG_TELEPORT = 0x02;

        public override byte PacketID { get; } = MinecraftProtocol.MOVE_ENTITY_ABSOLUTE_PACKET;

        public long EntityRuntimeId { get; set; }
        public byte Flags { get; set; } = 0;
        public Vector3 Position { get; set; }
        public float XRot { get; set; }
        public float YRot { get; set; }
        public float ZRot { get; set; }

        protected override void EncodePayload()
        {
            this.WriteEntityRuntimeId(this.EntityRuntimeId);
            this.WriteByte(this.Flags);
            this.WriteVector3(this.Position);
            this.WriteByteRotation(this.XRot);
            this.WriteByteRotation(this.YRot);
            this.WriteByteRotation(this.ZRot);
        }

        protected override void DecodePayload()
        {
            this.EntityRuntimeId = this.ReadEntityRuntimeId();
            this.Flags = this.ReadByte();
            this.Position = this.ReadVector3();
            this.XRot = this.ReadByteRotation();
            this.YRot = this.ReadByteRotation();
            this.ZRot = this.ReadByteRotation();
        }
    }
}
