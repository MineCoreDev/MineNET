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
                return InventorySlotPacket.ID;
            }
        }

        public uint InventoryId { get; set; }

        public uint Slot { get; set; }

        public Item Item { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteUVarInt(this.InventoryId);
            this.WriteUVarInt(this.Slot);
            this.WriteItem(this.Item);
        }
    }
}
