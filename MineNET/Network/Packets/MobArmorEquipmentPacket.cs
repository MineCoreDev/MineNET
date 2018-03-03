using MineNET.Items;

namespace MineNET.Network.Packets
{
    public class MobArmorEquipmentPacket : DataPacket
    {
        public const int ID = ProtocolInfo.MOB_ARMOR_EQUIPMENT_PACKET;

        public override byte PacketID
        {
            get
            {
                return MobArmorEquipmentPacket.ID;
            }
        }

        public long EntityRuntimeId { get; set; }

        public Item[] Items { get; set; } = new Item[4];

        public override void Encode()
        {
            base.Encode();

            this.WriteEntityRuntimeId(this.EntityRuntimeId);
            this.WriteItem(this.Items[0]);
            this.WriteItem(this.Items[1]);
            this.WriteItem(this.Items[2]);
            this.WriteItem(this.Items[3]);
        }
    }
}
