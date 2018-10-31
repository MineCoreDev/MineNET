using MineNET.Values;

namespace MineNET.Network.MinecraftPackets
{
    public class ItemFrameDropItemPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.ITEM_FRAME_DROP_ITEM_PACKET;

        public BlockCoordinate3D Position { get; set; }

        protected override void EncodePayload()
        {
            this.WriteBlockVector3(this.Position);
        }

        protected override void DecodePayload()
        {
            this.Position = this.ReadBlockVector3();
        }
    }
}
