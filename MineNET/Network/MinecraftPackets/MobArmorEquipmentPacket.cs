using MineNET.Items;

namespace MineNET.Network.MinecraftPackets
{
    public class MobArmorEquipmentPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.MOB_ARMOR_EQUIPMENT_PACKET;

        public long EntityRuntimeId { get; set; }

        public ItemStack[] Items { get; set; } = new ItemStack[4];

        protected override void EncodePayload()
        {
            this.WriteEntityRuntimeId(this.EntityRuntimeId);
            this.WriteItem(this.Items[0]);
            this.WriteItem(this.Items[1]);
            this.WriteItem(this.Items[2]);
            this.WriteItem(this.Items[3]);
        }

        protected override void DecodePayload()
        {

        }
    }
}
