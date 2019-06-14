using MineNET.Items;

namespace MineNET.Network.MinecraftPackets
{
    public class InventoryContentPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.INVENTORY_CONTENT_PACKET;

        public uint InventoryId { get; set; }

        public Item[] Items { get; set; }

        protected override void EncodePayload()
        {
            this.WriteUVarInt(this.InventoryId);
            this.WriteUVarInt((uint) this.Items.Length);
            for (int i = 0; i < this.Items.Length; ++i)
            {
                this.WriteItem(this.Items[i]);
            }
        }

        protected override void DecodePayload()
        {

        }
    }
}
