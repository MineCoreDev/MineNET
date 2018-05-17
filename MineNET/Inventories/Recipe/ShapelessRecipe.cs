using System.Collections.Generic;
using MineNET.Items;
using MineNET.Network.Packets;
using MineNET.Utils;
using MineNET.Values;

namespace MineNET.Inventories.Recipe
{
    public class ShapelessRecipe : IRecipe
    {
        public List<Item> Ingredients { get; }
        public Item Output { get; }

        public UUID UUID { get; }

        public int ID
        {
            get
            {
                return CraftingDataPacket.ENTRY_SHAPELESS;
            }
        }

        public ShapelessRecipe(List<Item> ingredients, Item output, UUID uuid = new UUID())
        {
            this.Ingredients = ingredients;
            this.Output = output;
            this.UUID = uuid;
        }

        public void Write(MCBEBinary stream)
        {
            stream.WriteSVarInt(this.ID);

            stream.WriteUVarInt((uint) this.Ingredients.Count);
            for (int i = 0; i < this.Ingredients.Count; ++i)
            {
                stream.WriteItem(this.Ingredients[i]);
            }
            stream.WriteVarInt(1);
            stream.WriteItem(this.Output);
            stream.WriteUUID(this.UUID);
        }
    }
}
