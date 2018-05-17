using System.Collections.Generic;
using MineNET.Inventories.Recipe;

namespace MineNET.Network.Packets
{
    public class CraftingDataPacket : DataPacket
    {
        public const int ENTRY_SHAPELESS = 0;
        public const int ENTRY_SHAPED = 1;
        public const int ENTRY_FURNACE = 2;
        public const int ENTRY_FURNACE_DATA = 3;
        public const int ENTRY_MULTI = 4;
        public const int ENTRY_SHULKER_BOX = 5;

        public const int ID = ProtocolInfo.CRAFTING_DATA_PACKET;

        public override byte PacketID
        {
            get
            {
                return CraftingDataPacket.ID;
            }
        }

        public List<IRecipe> Entries { get; set; } = new List<IRecipe>();
        public bool CleanRecipes { get; set; }

        public override void Encode()
        {
            base.Encode();

            this.WriteUVarInt((uint) this.Entries.Count);
            for (int i = 0; i < this.Entries.Count; ++i)
            {
                this.Entries[i].Write(this);
            }
            this.WriteBool(this.CleanRecipes);
        }
    }
}
