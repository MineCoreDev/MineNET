using MineNET.Values;

namespace MineNET.Network.Packets
{
    public class BlockEntityDataPacket : DataPacket
    {
        public override byte PacketID
        {
            get
            {
                return ProtocolInfo.BLOCK_ENTITY_DATA_PACKET;
            }
        }

        public Vector3i Position { get; set; }
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

            this.Position = this.ReadBlockVector3i();
            this.Namedtag = this.ReadBytes();
        }
    }
}
