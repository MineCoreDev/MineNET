using System;
using System.Collections.Generic;
using MineNET.Entities.Players;
using MineNET.Items;
using MineNET.Network.Packets;

namespace MineNET.Inventories.Recipe
{
    public class CraftingManager
    {
        public List<IRecipe> Recipes { get; } = new List<IRecipe>();

        public CraftingManager()
        {
            this.RegisterRecipe();
        }

        public void RegisterRecipe()
        {
            this.AddShapedRecipe(new Item[] { Item.Get(5, 0, 4) }, new object[] { new string[] { "#" }, "#", Item.Get(17) });
        }

        public ShapedRecipe AddShapedRecipe(Item[] output, object[] recipeComponents)
        {
            if (!(recipeComponents[0] is string[]))
            {
                throw new InvalidOperationException("The beginning of object array is string array");
            }
            string[] recipeKey = (string[]) recipeComponents[0];
            int height = recipeKey.Length;
            int width = recipeKey[0].Length;

            for (int i = 0; i < height; ++i)
            {
                if (width != recipeKey[0].Length)
                {
                    throw new ArgumentOutOfRangeException("recipe is 2 * 2 or 3 * 3 size");
                }
            }

            Dictionary<string, Item> registry = new Dictionary<string, Item>();
            registry.Add(" ", Item.Get(0));
            for (int i = 1; i < recipeComponents.Length; i += 2)
            {
                try
                {
                    registry.Add((string) recipeComponents[i], (Item) recipeComponents[i + 1]);
                }
                catch
                {
                    throw new InvalidOperationException("Secound argument is 'string[] recipe, string key, Item item...'");
                }
            }

            Item[] recipeItems = new Item[height * width];

            for (int i = 0; i < height; ++i)
            {
                for (int j = 0; j < width; ++j)
                {
                    string s = recipeKey[i].Substring(j, 1);
                    if (!registry.ContainsKey(s))
                    {
                        throw new ArgumentNullException("recipes key not found");
                    }
                    recipeItems[i * 3 + j] = registry[s];
                }
            }

            ShapedRecipe recipe = new ShapedRecipe(width, height, recipeItems, output);
            this.Recipes.Add(recipe);
            return recipe;
        }

        public ShapelessRecipe AddShapelessRecipe(Item[] output, Item[] recipeItems)
        {
            ShapelessRecipe recipe = new ShapelessRecipe(recipeItems, output);
            this.Recipes.Add(recipe);
            return recipe;
        }

        public void SendPacket(Player player)
        {
            CraftingDataPacket pk = new CraftingDataPacket();
            pk.Entries = this.Recipes;
            player.SendPacket(pk);
        }
    }
}
