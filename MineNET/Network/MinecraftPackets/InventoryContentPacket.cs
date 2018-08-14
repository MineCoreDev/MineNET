using MineNET.Items;

namespace MineNET.Network.MinecraftPackets
{
    public class InventoryContentPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.INVENTORY_CONTENT_PACKET;

        public uint InventoryId { get; set; }

        public ItemStack[] Items { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteUVarInt(this.InventoryId);
            this.WriteUVarInt((uint) this.Items.Length);
            for (int i = 0; i < this.Items.Length; ++i)
            {
                this.WriteItem(this.Items[i]);
            }
        }
    }
}
