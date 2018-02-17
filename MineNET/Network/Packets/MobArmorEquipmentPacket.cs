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
                return ID;
            }
        }

        long entityRuntimeId;
        public long EntityRuntimeId
        {
            get
            {
                return this.entityRuntimeId;
            }

            set
            {
                this.entityRuntimeId = value;
            }
        }

        Item[] items = new Item[4];
        public Item[] Items
        {
            get
            {
                return this.items;
            }

            set
            {
                this.items = value;
            }
        }

        public override void Encode()
        {
            base.Encode();

            this.WriteEntityRuntimeId(this.entityRuntimeId);
            this.WriteItem(this.items[0]);
            this.WriteItem(this.items[1]);
            this.WriteItem(this.items[2]);
            this.WriteItem(this.items[3]);
        }
    }
}
