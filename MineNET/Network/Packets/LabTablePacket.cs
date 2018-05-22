using MineNET.Values;

namespace MineNET.Network.Packets
{
    public class LabTablePacket : DataPacket
    {
        public const int ID = ProtocolInfo.LAB_TABLE_PACKET;

        public override byte PacketID
        {
            get
            {
                return LabTablePacket.ID;
            }
        }

        public byte UselessByte { get; set; }
        public Vector3i Position { get; set; }
        public byte ReactionType { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteByte(this.UselessByte);
            this.WriteSBlockVector3(this.Position);
            this.WriteByte(this.ReactionType);
        }
    }
}
