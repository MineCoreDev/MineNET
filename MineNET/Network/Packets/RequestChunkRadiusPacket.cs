namespace MineNET.Network.Packets
{
    public class RequestChunkRadiusPacket : DataPacket
    {
        public const int ID = ProtocolInfo.REQUEST_CHUNK_RADIUS_PACKET;

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
        }

        public override void Decode()
        {
            base.Decode();

            this.radius = ReadSVarInt();
        }
    }
}
