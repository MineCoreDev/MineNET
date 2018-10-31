using MineNET.Values;

namespace MineNET.Network.MinecraftPackets
{
    public class BlockEntityDataPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.BLOCK_ENTITY_DATA_PACKET;

        public BlockCoordinate3D Position { get; set; }
        public byte[] Namedtag { get; set; }

        protected override void EncodePayload()
        {
            this.WriteBlockVector3(this.Position);
            this.WriteBytes(this.Namedtag);
        }

        protected override void DecodePayload()
        {
            this.Position = this.ReadBlockVector3();
            this.Namedtag = this.ReadBytes();
        }
    }
}
