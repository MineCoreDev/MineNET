using MineNET.Values;

namespace MineNET.Network.Packets
{
    public class AddPaintingPacket : DataPacket
    {
        public const int ID = ProtocolInfo.ADD_PAINTING_PACKET;

        public override byte PacketID
        {
            get
            {
                return AddPaintingPacket.ID;
            }
        }

        public long EntityUniqueId { get; set; }
        public long EntityRuntimeId { get; set; }
        public Vector3i Position { get; set; }
        public int Direction { get; set; }
        public string Title { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteEntityUniqueId(this.EntityUniqueId);
            this.WriteEntityRuntimeId(this.EntityRuntimeId);
            this.WriteBlockVector3(this.Position);
            this.WriteVarInt(this.Direction);
            this.WriteString(this.Title);
        }
    }
}
