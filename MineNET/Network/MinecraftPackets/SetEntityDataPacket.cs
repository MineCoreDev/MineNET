using MineNET.Entities.Metadata;

namespace MineNET.Network.MinecraftPackets
{
    public class SetEntityDataPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.SET_ENTITY_DATA_PACKET;

        public long EntityRuntimeId { get; set; }
        public EntityMetadataManager EntityData { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteEntityRuntimeId(this.EntityRuntimeId);
            this.WriteEntityMetadata(this.EntityData);
        }
    }
}
