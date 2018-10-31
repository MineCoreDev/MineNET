using MineNET.Values;

namespace MineNET.Network.MinecraftPackets
{
    public class AddPaintingPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.ADD_PAINTING_PACKET;

        public long EntityUniqueId { get; set; }
        public long EntityRuntimeId { get; set; }
        public BlockCoordinate3D Position { get; set; }
        public int Direction { get; set; }
        public string Title { get; set; }

        protected override void EncodePayload()
        {
            this.WriteEntityUniqueId(this.EntityUniqueId);
            this.WriteEntityRuntimeId(this.EntityRuntimeId);
            this.WriteBlockVector3(this.Position);
            this.WriteVarInt(this.Direction);
            this.WriteString(this.Title);
        }

        protected override void DecodePayload()
        {

        }
    }
}
