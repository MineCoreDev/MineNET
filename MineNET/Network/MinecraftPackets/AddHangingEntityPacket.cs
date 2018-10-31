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

        protected override void EncodePayload()
        {
            this.WriteEntityUniqueId(this.EntityUniqueId);
            this.WriteEntityRuntimeId(this.EntityRuntimeId);
            this.WriteBlockVector3(this.Position);
            this.WriteVarInt(this.Direction);
        }

        protected override void DecodePayload()
        {
            this.EntityUniqueId = this.ReadEntityUniqueId();
            this.EntityRuntimeId = this.ReadEntityRuntimeId();
            this.Position = this.ReadBlockVector3();
            this.Direction = this.ReadVarInt();
        }
    }
}
