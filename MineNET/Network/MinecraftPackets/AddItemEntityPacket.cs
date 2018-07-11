using MineNET.Entities.Metadata;
using MineNET.Items;
using MineNET.Values;

namespace MineNET.Network.MinecraftPackets
{
    public class AddItemEntityPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.ADD_ITEM_ENTITY_PACKET;

        public long EntityUniqueId { get; set; }
        public long EntityRuntimeId { get; set; }
        public ItemStack ItemStack { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Motion { get; set; }
        public EntityMetadataManager Metadata { get; set; }
        public bool IsFromFishing { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteEntityUniqueId(this.EntityUniqueId);
            this.WriteEntityRuntimeId(this.EntityRuntimeId);
            this.WriteItem(this.ItemStack);
            this.WriteVector3(this.Position);
            this.WriteVector3(this.Motion);
            this.WriteEntityMetadata(this.Metadata);
            this.WriteBool(this.IsFromFishing);
        }
    }
}
