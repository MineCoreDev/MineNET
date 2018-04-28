using MineNET.Values;

namespace MineNET.Network.Packets
{
    public class UpdateBlockPacket : DataPacket
    {
        public const int FLAG_NONE = 0x0;
        public const int FLAG_NEIGHBORS = 0x1;
        public const int FLAG_NETWORK = 0x2;
        public const int FLAG_NOGRAPHIC = 0x4;
        public const int FLAG_PRIORITY = 0x8;

        public const int FLAG_ALL = UpdateBlockPacket.FLAG_NEIGHBORS | UpdateBlockPacket.FLAG_NETWORK;
        public const int FLAG_ALL_PRIORITY = UpdateBlockPacket.FLAG_ALL | UpdateBlockPacket.FLAG_PRIORITY;

        public const int DATA_LAYER_NORMAL = 0;
        public const int DATA_LAYER_LIQUID = 1;

        public const int ID = ProtocolInfo.UPDATE_BLOCK_PACKET;

        public override byte PacketID
        {
            get
            {
                return UpdateBlockPacket.ID;
            }
        }

        public Vector3i Position { get; set; }
        public int RuntimeId { get; set; }
        public int Flags { get; set; }
        public int DataLayerId { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteBlockVector3(this.Position);
            this.WriteUVarInt((uint) this.RuntimeId);
            this.WriteUVarInt((uint) this.Flags);
            this.WriteUVarInt((uint) this.DataLayerId);
        }
    }
}
