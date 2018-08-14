using MineNET.Items;
using MineNET.Network.MinecraftPackets;
using MineNET.Values;

namespace MineNET.Inventories.Recipe
{
    public class ShapedRecipe : IRecipe
    {
        public int Width { get; }
        public int Height { get; }

        public ItemStack[] RecipeItems { get; }
        public ItemStack[] Output { get; }

        public UUID UUID { get; }

        public int ID
        {
            get
            {
                return CraftingDataPacket.ENTRY_SHAPED;
            }
        }

        public ShapedRecipe(int width, int height, ItemStack[] recipeItems, ItemStack[] output, UUID uuid = new UUID())
        {
            this.Width = width;
            this.Height = height;
            this.RecipeItems = recipeItems;
            this.Output = output;
            this.UUID = uuid;
        }

        public void Write(MinecraftPacket stream)
        {
            stream.WriteSVarInt(this.ID);

            stream.WriteSVarInt(this.Width);
            stream.WriteSVarInt(this.Height);

            for (int i = 0; i < this.RecipeItems.Length; ++i)
            {
                stream.WriteItem(this.RecipeItems[i]);
            }

            stream.WriteUVarInt((uint) this.Output.Length);
            for (int i = 0; i < this.Output.Length; ++i)
            {
                stream.WriteItem(this.Output[i]);
            }
            stream.WriteUUID(this.UUID);
        }
    }
}
