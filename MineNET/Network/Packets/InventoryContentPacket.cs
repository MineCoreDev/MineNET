using MineNET.Items;

namespace MineNET.Network.Packets
{
    public class InventoryContentPacket : DataPacket
    {
        public const int ID = ProtocolInfo.INVENTORY_CONTENT_PACKET;

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

        Item[] items;
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

            this.WriteUVarInt(this.inventoryId);
            this.WriteUVarInt((uint) this.items.Length);
            for (int i = 0; i < this.items.Length; ++i)
            {
                this.WriteItem(this.items[i]);
            }
        }
    }
}
