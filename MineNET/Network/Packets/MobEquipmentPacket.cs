using MineNET.Items;

namespace MineNET.Network.Packets
{
    public class MobEquipmentPacket : DataPacket
    {
        public const int ID = ProtocolInfo.MOB_EQUIPMENT_PACKET;

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

        Item item;
        public Item Item
        {
            get
            {
                return this.item;
            }

            set
            {
                this.item = value;
            }
        }

        byte inventorySlot;
        public byte InventorySlot
        {
            get
            {
                return this.inventorySlot;
            }

            set
            {
                this.inventorySlot = value;
            }
        }

        byte hotbarSlot;
        public byte HotbarSlot
        {
            get
            {
                return this.hotbarSlot;
            }

            set
            {
                this.hotbarSlot = value;
            }
        }

        byte windowId;
        public byte WindowId
        {
            get
            {
                return this.windowId;
            }

            set
            {
                this.windowId = value;
            }
        }

        public override void Decode()
        {
            base.Decode();

            this.entityRuntimeId = this.ReadEntityRuntimeId();
            this.item = this.ReadItem();
            this.inventorySlot = this.ReadByte();
            this.hotbarSlot = this.ReadByte();
            this.windowId = this.ReadByte();
        }

        public override void Encode()
        {
            base.Encode();

            this.WriteEntityRuntimeId(this.entityRuntimeId);
            this.WriteItem(this.item);
            this.WriteByte(this.inventorySlot);
            this.WriteByte(this.hotbarSlot);
            this.WriteByte(this.windowId);
        }
    }
}
