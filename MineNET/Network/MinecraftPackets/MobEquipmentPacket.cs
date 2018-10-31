using MineNET.Items;

namespace MineNET.Network.MinecraftPackets
{
    public class MobEquipmentPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.MOB_EQUIPMENT_PACKET;

        public long EntityRuntimeId { get; set; }
        public ItemStack Item { get; set; }
        public byte InventorySlot { get; set; }
        public byte HotbarSlot { get; set; }
        public byte WindowId { get; set; }

        protected override void EncodePayload()
        {
            this.WriteEntityRuntimeId(this.EntityRuntimeId);
            this.WriteItem(this.Item);
            this.WriteByte(this.InventorySlot);
            this.WriteByte(this.HotbarSlot);
            this.WriteByte(this.WindowId);
        }

        protected override void DecodePayload()
        {
            this.EntityRuntimeId = this.ReadEntityRuntimeId();
            this.Item = this.ReadItem();
            this.InventorySlot = this.ReadByte();
            this.HotbarSlot = this.ReadByte();
            this.WindowId = this.ReadByte();
        }
    }
}
