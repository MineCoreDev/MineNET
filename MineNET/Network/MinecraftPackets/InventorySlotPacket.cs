using MineNET.Items;

namespace MineNET.Network.MinecraftPackets
{
    public class InventorySlotPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.INVENTORY_SLOT_PACKET;

        public uint InventoryId { get; set; }

        public uint Slot { get; set; }

        public Item Item { get; set; }

        protected override void EncodePayload()
        {
            this.WriteUVarInt(this.InventoryId);
            this.WriteUVarInt(this.Slot);
            this.WriteItem(this.Item);
        }

        protected override void DecodePayload()
        {

        }
    }
}
