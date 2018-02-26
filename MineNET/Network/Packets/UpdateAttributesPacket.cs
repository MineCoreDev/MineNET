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
                return ID;
            }
        }

        long entityRuntimeId;
        public long EntityRuntimeId
        {
            get
            {
                return entityRuntimeId;
            }

            set
            {
                entityRuntimeId = value;
            }
        }

        EntityAttribute[] attributes;
        public EntityAttribute[] Attributes
        {
            get
            {
                return attributes;
            }

            set
            {
                attributes = value;
            }
        }

        public override void Encode()
        {
            base.Encode();

            WriteEntityRuntimeId(entityRuntimeId);
            WriteAttributes(attributes);
        }
    }
}
