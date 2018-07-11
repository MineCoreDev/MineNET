using MineNET.Values;

namespace MineNET.Network.MinecraftPackets
{
    public class AddHangingEntityPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.ADD_HANGING_ENTITY_PACKET;

        public long EntityUniqueId { get; set; }
        public long EntityRuntimeId { get; set; }
        public BlockCoordinate3D Position { get; set; }
        public int Direction { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteEntityUniqueId(this.EntityUniqueId);
            this.WriteEntityRuntimeId(this.EntityRuntimeId);
            this.WriteBlockVector3(this.Position);
            this.WriteVarInt(this.Direction);
        }

        public override void Decode()
        {
            base.Decode();

            this.EntityUniqueId = this.ReadEntityUniqueId();
            this.EntityRuntimeId = this.ReadEntityRuntimeId();
            this.Position = this.ReadBlockVector3();
            this.Direction = this.ReadVarInt();
        }
    }
}
