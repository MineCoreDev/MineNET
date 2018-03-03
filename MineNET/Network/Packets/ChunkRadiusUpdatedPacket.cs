namespace MineNET.Network.Packets
{
    public class ChunkRadiusUpdatedPacket : DataPacket
    {
        public const int ID = ProtocolInfo.CHUNK_RADIUS_UPDATED_PACKET;

        public override byte PacketID
        {
            get
            {
                return ChunkRadiusUpdatedPacket.ID;
            }
        }

        public int Radius { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteSVarInt(this.Radius);
        }
    }
}
