using MineNET.Values;

namespace MineNET.Network.MinecraftPackets
{
    public class BlockEntityDataPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.BLOCK_ENTITY_DATA_PACKET;

        public BlockCoordinate3D Position { get; set; }
        public byte[] Namedtag { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteBlockVector3(this.Position);
            this.WriteBytes(this.Namedtag);
        }

        public override void Decode()
        {
            base.Decode();

            this.Position = this.ReadBlockVector3();
            this.Namedtag = this.ReadBytes();
        }
    }
}
