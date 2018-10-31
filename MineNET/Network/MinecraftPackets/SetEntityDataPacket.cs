using MineNET.Entities.Metadata;

namespace MineNET.Network.MinecraftPackets
{
    public class SetEntityDataPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.SET_ENTITY_DATA_PACKET;

        public long EntityRuntimeId { get; set; }
        public EntityMetadataManager EntityData { get; set; }

        protected override void EncodePayload()
        {
            this.WriteEntityRuntimeId(this.EntityRuntimeId);
            this.WriteEntityMetadata(this.EntityData);
        }

        protected override void DecodePayload()
        {

        }
    }
}
