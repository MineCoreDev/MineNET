using System.Collections.Generic;
using MineNET.Items;
using MineNET.Network.Packets;
using MineNET.Utils;
using MineNET.Values;

namespace MineNET.Inventories.Recipe
{
    public class ShapedRecipe : IRecipe
    {
        public int Width { get; }
        public int Height { get; }

        public Dictionary<int, Dictionary<int, Item>> Ingredients { get; } = new Dictionary<int, Dictionary<int, Item>>();
        public Item Output { get; }

        public UUID UUID { get; }

        public int ID
        {
            get
            {
                return CraftingDataPacket.ENTRY_SHAPED;
            }
        }

        public void Write(MCBEBinary stream)
        {
            stream.WriteSVarInt(this.ID);

            stream.WriteSVarInt(this.Width);
            stream.WriteSVarInt(this.Height);

            for (int x = 0; x < this.Width; ++x)
            {
                for (int z = 0; z < this.Height; ++z)
                {
                    stream.WriteItem(this.Ingredients[x][z]);
                }
            }

            stream.WriteVarInt(1);
            stream.WriteItem(this.Output);
            stream.WriteUUID(this.UUID);
        }
    }
}
