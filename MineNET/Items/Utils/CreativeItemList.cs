using System;
using System.Linq;
using System.Text;
using MineNET.Resources;
using Newtonsoft.Json.Linq;

namespace MineNET.Items
{
    public class CreativeItemList
    {
        private CreativeItemList()
        {

        }

        public static void AddCreativeItem(Item item)
        {
            MineNET_Registries.Creative.Add(item);
        }

        public static void RemoveCreativeItem(Item item)
        {
            MineNET_Registries.Creative.Remove(item);
        }

        public static void RemoveCreativeItem(int index)
        {
            MineNET_Registries.Creative.RemoveAt(index);
        }

        public static void AddCreativeItems(params Item[] items)
        {
            for (int i = 0; i < items.Length; ++i)
            {
                CreativeItemList.AddCreativeItem(items[i]);
            }
        }

        public static void RemoveAllCreativeItems()
        {
            MineNET_Registries.Creative.Clear();
        }

        public static Item[] GetCreativeItems()
        {
            return MineNET_Registries.Creative.ToArray();
        }

        public static void LoadCreativeItems()
        {
            string data = Encoding.UTF8.GetString(Resource.CreativeItems);
            JObject json = JObject.Parse(data);
            JToken items = json.GetValue("items");
            foreach (JObject item in items)
            {
                int id = item.Value<int>("id");
                int damage = item.Value<int>("damage");
                string tags = item.Value<string>("nbt_hex");
                byte[] nbt = null;
                if (!string.IsNullOrEmpty(tags))
                {
                    nbt = tags.Chunks(2).Select(x => Convert.ToByte(new string(x.ToArray()), 16)).ToArray();
                }

                CreativeItemList.AddCreativeItem(Item.Get(id, damage, 1, nbt));
            }
        }
    }
}
