using MineNET.Items;
using MineNET.Network.MinecraftPackets;
using MineNET.Values;

namespace MineNET.Inventories.Recipe
{
    public class ShapelessRecipe : IRecipe
    {
        public Item[] RecipeItems { get; }
        public Item[] Output { get; }

        public UUID UUID { get; }

        public int ID
        {
            get
            {
                return CraftingDataPacket.ENTRY_SHAPELESS;
            }
        }

        public ShapelessRecipe(Item[] recipeItems, Item[] output, UUID uuid = new UUID())
        {
            this.RecipeItems = recipeItems;
            this.Output = output;
            this.UUID = uuid;
        }

        public void Write(MinecraftPacket stream)
        {
            stream.WriteSVarInt(this.ID);

            stream.WriteUVarInt((uint) this.RecipeItems.Length);
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
