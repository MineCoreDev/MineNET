using MineNET.Values;

namespace MineNET.Network.Packets
{
    public class UpdateBlockPacket : DataPacket
    {
        public const int FLAG_NONE = 0b0000;
        public const int FLAG_NEIGHBORS = 0b0001;
        public const int FLAG_NETWORK = 0b0010;
        public const int FLAG_NOGRAPHIC = 0b0100;
        public const int FLAG_PRIORITY = 0b1000;

        public const int FLAG_ALL = UpdateBlockPacket.FLAG_NEIGHBORS | UpdateBlockPacket.FLAG_NETWORK;
        public const int FLAG_ALL_PRIORITY = UpdateBlockPacket.FLAG_ALL | UpdateBlockPacket.FLAG_PRIORITY;

        public const int ID = ProtocolInfo.UPDATE_BLOCK_PACKET;

        public override byte PacketID
        {
            get
            {
                return UpdateBlockPacket.ID;
            }
        }

        public Vector3i Vector3 { get; set; }
        public int BlockId { get; set; }
        public int BlockData { get; set; }
        public int Flags { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteBlockVector3(this.Vector3);
            this.WriteUVarInt((uint) this.BlockId);
            this.WriteUVarInt((uint) ((this.Flags << 4) | this.BlockData));
        }
    }
}
