using MineNET.Values;

namespace MineNET.Network.MinecraftPackets
{
    public class UpdateBlockPacket : MinecraftPacket
    {
        public const uint FLAG_NONE = 0x0;
        public const uint FLAG_NEIGHBORS = 0x1;
        public const uint FLAG_NETWORK = 0x2;
        public const uint FLAG_NOGRAPHIC = 0x4;
        public const uint FLAG_PRIORITY = 0x8;

        public const uint FLAG_ALL = UpdateBlockPacket.FLAG_NEIGHBORS | UpdateBlockPacket.FLAG_NETWORK;
        public const uint FLAG_ALL_PRIORITY = UpdateBlockPacket.FLAG_ALL | UpdateBlockPacket.FLAG_PRIORITY;

        public const uint DATA_LAYER_NORMAL = 0;
        public const uint DATA_LAYER_LIQUID = 1;

        public override byte PacketID { get; } = MinecraftProtocol.UPDATE_BLOCK_PACKET;

        public BlockCoordinate3D Position { get; set; }
        public uint RuntimeId { get; set; }
        public uint Flags { get; set; }
        public uint DataLayerId { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteBlockVector3(this.Position);
            this.WriteUVarInt(this.RuntimeId);
            this.WriteUVarInt(this.Flags);
            this.WriteUVarInt(this.DataLayerId);
        }
    }
}
