using MineNET.Items;

namespace MineNET.Network.Packets
{
    public class InventorySlotPacket : DataPacket
    {
        public const int ID = ProtocolInfo.INVENTORY_SLOT_PACKET;

        public override byte PacketID
        {
            get
            {
                return ID;
            }
        }

        uint inventoryId;
        public uint InventoryId
        {
            get
            {
                return this.inventoryId;
            }

            set
            {
                this.inventoryId = value;
            }
        }

        uint slot;
        public uint Slot
        {
            get
            {
                return this.slot;
            }

            set
            {
                this.slot = value;
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

        public override void Encode()
        {
            base.Encode();

            this.WriteUVarInt(this.inventoryId);
            this.WriteUVarInt(this.slot);
            this.WriteItem(this.item);
        }
    }
}
