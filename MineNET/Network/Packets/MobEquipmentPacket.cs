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
                return MobEquipmentPacket.ID;
            }
        }

        public long EntityRuntimeId { get; set; }

        public Item Item { get; set; }

        public byte InventorySlot { get; set; }

        public byte HotbarSlot { get; set; }

        public byte WindowId { get; set; }

        public override void Decode()
        {
            base.Decode();

            this.EntityRuntimeId = this.ReadEntityRuntimeId();
            this.Item = this.ReadItem();
            this.InventorySlot = this.ReadByte();
            this.HotbarSlot = this.ReadByte();
            this.WindowId = this.ReadByte();
        }

        public override void Encode()
        {
            base.Encode();

            this.WriteEntityRuntimeId(this.EntityRuntimeId);
            this.WriteItem(this.Item);
            this.WriteByte(this.InventorySlot);
            this.WriteByte(this.HotbarSlot);
            this.WriteByte(this.WindowId);
        }
    }
}
