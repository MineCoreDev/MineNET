using MineNET.Items;

namespace MineNET.Network.MinecraftPackets
{
    public class InventorySlotPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.INVENTORY_SLOT_PACKET;

        public uint InventoryId { get; set; }

        public uint Slot { get; set; }

        public ItemStack Item { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteUVarInt(this.InventoryId);
            this.WriteUVarInt(this.Slot);
            this.WriteItem(this.Item);
        }
    }
}
