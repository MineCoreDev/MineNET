using MineNET.Items;

namespace MineNET.Network.MinecraftPackets
{
    public class MobArmorEquipmentPacket : MinecraftPacket
    {
        public override byte PacketID { get; } = MinecraftProtocol.MOB_ARMOR_EQUIPMENT_PACKET;

        public long EntityRuntimeId { get; set; }

        public Item[] Items { get; set; } = new Item[4];

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
