namespace MineNET.Network.Packets
{
    public class ChunkRadiusUpdatedPacket : DataPacket
    {
        public const int ID = ProtocolInfo.CHUNK_RADIUS_UPDATED_PACKET;

        public override byte PacketID
        {
            get
            {
                return ID;
            }
        }

        int radius;
        public int Radius
        {
            get
            {
                return radius;
            }

            set
            {
                radius = value;
            }
        }

        public override void Encode()
        {
            base.Encode();

            WriteVarInt(radius);
        }
    }
}
