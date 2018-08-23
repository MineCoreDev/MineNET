using MineNET.Values;

namespace MineNET.Network.MinecraftPackets
{
    public class ItemFrameDropItemPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.ITEM_FRAME_DROP_ITEM_PACKET;

        public BlockCoordinate3D Position { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteBlockVector3(this.Position);
        }

        public override void Decode()
        {
            base.Decode();

            this.Position = this.ReadBlockVector3();
        }
    }
}
