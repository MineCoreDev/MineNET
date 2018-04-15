namespace MineNET.Network.Packets
{
    public class RemoveEntityPacket : DataPacket
    {
        public const int ID = ProtocolInfo.REMOVE_ENTITY_PACKET;

        public override byte PacketID
        {
            get
            {
                return AddEntityPacket.ID;
            }
        }

        public long EntityUniqueId { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteEntityUniqueId(this.EntityUniqueId);
        }

        public override void Decode()
        {
            base.Decode();

            this.EntityUniqueId = this.ReadEntityUniqueId();
        }
    }
}
