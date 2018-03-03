namespace MineNET.Network.Packets
{
    public class RequestChunkRadiusPacket : DataPacket
    {
        public const int ID = ProtocolInfo.REQUEST_CHUNK_RADIUS_PACKET;

        public override byte PacketID
        {
            get
            {
                return RequestChunkRadiusPacket.ID;
            }
        }

        public int Radius { get; set; }

        public override void Decode()
        {
            base.Decode();

            this.Radius = ReadSVarInt();
        }
    }
}
