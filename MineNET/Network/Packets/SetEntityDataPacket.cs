using MineNET.Entities.Metadata;

namespace MineNET.Network.Packets
{
    public class SetEntityDataPacket : DataPacket
    {
        public const int ID = ProtocolInfo.SET_ENTITY_DATA_PACKET;

        public override byte PacketID
        {
            get
            {
                return SetEntityDataPacket.ID;
            }
        }

        public long EntityRuntimeId { get; set; }

        public EntityMetadataManager EntityData { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteEntityRuntimeId(this.EntityRuntimeId);
            //TODO : EntityMetadataManager
        }
    }
}
