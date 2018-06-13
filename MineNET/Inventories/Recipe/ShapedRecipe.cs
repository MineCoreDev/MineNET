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

        public Item[] RecipeItems { get; }
        public Item Output { get; }

        public UUID UUID { get; }

        public int ID
        {
            get
            {
                return CraftingDataPacket.ENTRY_SHAPED;
            }
        }

        public ShapedRecipe(int width, int height, Item[] recipeItems, Item output, UUID uuid = new UUID())
        {
            this.Width = width;
            this.Height = height;
            this.RecipeItems = recipeItems;
            this.Output = output;
            this.UUID = uuid;
        }

        public void Write(MCBEBinary stream)
        {
            stream.WriteSVarInt(this.ID);

            stream.WriteSVarInt(this.Width);
            stream.WriteSVarInt(this.Height);

            for (int i = 0; i < this.RecipeItems.Length; ++i)
            {
                stream.WriteItem(this.RecipeItems[i]);
            }

            stream.WriteVarInt(1);
            stream.WriteItem(this.Output);
            stream.WriteUUID(this.UUID);
        }
    }
}
