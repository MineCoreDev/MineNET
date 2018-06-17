using MineNET.Values;

namespace MineNET.Network.Packets
{
    public class AddHangingEntityPacket : DataPacket
    {
        public const int ID = ProtocolInfo.ADD_HANGING_ENTITY_PACKET;

        public override byte PacketID
        {
            get
            {
                return AddHangingEntityPacket.ID;
            }
        }

        public long EntityUniqueId { get; set; }
        public long EntityRuntimeId { get; set; }
        public Vector3i Position { get; set; }
        public int Direction { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteEntityUniqueId(this.EntityUniqueId);
            this.WriteEntityRuntimeId(this.EntityRuntimeId);
            this.WriteBlockVector3(this.Position);
            this.WriteVarInt(this.Direction);
        }

        public override void Decode()
        {
            base.Decode();

            this.EntityUniqueId = this.ReadEntityUniqueId();
            this.EntityRuntimeId = this.ReadEntityRuntimeId();
            this.Position = this.ReadBlockVector3i();
            this.Direction = this.ReadVarInt();
        }
    }
}
