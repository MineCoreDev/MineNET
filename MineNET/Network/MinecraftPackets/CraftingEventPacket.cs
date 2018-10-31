using MineNET.Items;
using MineNET.Values;

namespace MineNET.Network.MinecraftPackets
{
    public class CraftingEventPacket : MinecraftPacket
    {
        public const int ENTRY_SHAPELESS = 0;
        public const int ENTRY_SHAPED = 1;
        public const int ENTRY_FURNACE = 2;
        public const int ENTRY_FURNACE_DATA = 3;
        public const int ENTRY_MULTI = 4;
        public const int ENTRY_SHULKER_BOX = 5;

        public override byte PacketID { get; } = MinecraftProtocol.CRAFTING_EVENT_PACKET;

        public byte WindowId { get; set; }
        public int Type { get; set; }
        public UUID UUID { get; set; }
        public ItemStack[] Input { get; set; }
        public ItemStack[] Output { get; set; }

        protected override void EncodePayload()
        {

        }

        protected override void DecodePayload()
        {
            this.WindowId = this.ReadByte();
            this.Type = this.ReadSVarInt();
            this.UUID = this.ReadUUID();

            this.Input = new ItemStack[this.ReadUVarInt()];
            for (int i = 0; i < this.Input.Length; ++i)
            {
                this.Input[i] = this.ReadItem();
            }

            this.Output = new ItemStack[this.ReadUVarInt()];
            for (int i = 0; i < this.Output.Length; ++i)
            {
                this.Output[i] = this.ReadItem();
            }
        }
    }
}