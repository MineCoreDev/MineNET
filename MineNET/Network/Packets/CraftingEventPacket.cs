using MineNET.Items;
using MineNET.Values;

namespace MineNET.Network.Packets
{
    public class CraftingEventPacket : DataPacket
    {
        public const int ENTRY_SHAPELESS = 0;
        public const int ENTRY_SHAPED = 1;
        public const int ENTRY_FURNACE = 2;
        public const int ENTRY_FURNACE_DATA = 3;
        public const int ENTRY_MULTI = 4;
        public const int ENTRY_SHULKER_BOX = 5;

        public const int ID = ProtocolInfo.CRAFTING_EVENT_PACKET;

        public override byte PacketID
        {
            get
            {
                return CraftingEventPacket.ID;
            }
        }

        public byte WindowId { get; set; }
        public int Type { get; set; }
        public UUID UUID { get; set; }
        public Item[] Input { get; set; }
        public Item[] Output { get; set; }

        public override void Decode()
        {
            base.Decode();

            this.WindowId = this.ReadByte();
            this.Type = this.ReadSVarInt();
            this.UUID = this.ReadUUID();

            this.Input = new Item[this.ReadUVarInt()];
            for (int i = 0; i < this.Input.Length; ++i)
            {
                this.Input[i] = this.ReadItem();
            }

            this.Output = new Item[this.ReadUVarInt()];
            for (int i = 0; i < this.Output.Length; ++i)
            {
                this.Output[i] = this.ReadItem();
            }
        }
    }
}
