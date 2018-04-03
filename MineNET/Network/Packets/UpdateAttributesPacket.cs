using MineNET.Entities.Attributes;

namespace MineNET.Network.Packets
{
    public class UpdateAttributesPacket : DataPacket
    {
        public const int ID = ProtocolInfo.UPDATE_ATTRIBUTES_PACKET;

        public override byte PacketID
        {
            get
            {
                return UpdateAttributesPacket.ID;
            }
        }

        public long EntityRuntimeId { get; set; }

        public EntityAttributeDictionary Attributes { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteEntityRuntimeId(this.EntityRuntimeId);
            this.WriteAttributes(this.Attributes);
        }
    }
}
