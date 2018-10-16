using System;
using System.Linq;
using System.Reflection;
using System.Text;
using MineNET.Blocks;
using MineNET.Data;
using MineNET.Entities;
using MineNET.Entities.Players;
using MineNET.Resources;
using MineNET.Values;
using MineNET.Worlds;
using Newtonsoft.Json.Linq;

namespace MineNET.Items
{
    public class Item
    {
        private Block block = null;

        public int ID { get; }

        public static Item Get(int id)
        {
            if (MineNET_Registries.Item.ContainsKey(id))
            {
                return MineNET_Registries.Item[id];
            }
            else
            {
                return new ItemBlock(Block.Get(BlockIDs.AIR));
            }
        }

        public static Item Get(string name)
        {
            string[] data = name.Replace("minecraft:", "").Replace(" ", "_").ToUpper().Split(':');
            int id = 0;

            if (data.Length == 1)
            {
                int.TryParse(data[0], out id);
            }

            ItemIDs factory = new ItemIDs();
            FieldInfo info = factory.GetType().GetField(data[0]);
            if (info != null)
            {
                id = (int) info.GetValue(factory);
            }
            else
            {
                BlockIDs factory2 = new BlockIDs();
                FieldInfo info2 = factory2.GetType().GetField(data[0]);
                if (info2 != null)
                {
                    id = (int) info2.GetValue(factory2);
                    if (id > 255)
                    {
                        id = -id + 255;
                    }
                }
            }

            Item item = Item.Get(id);
            return item;
        }

        public static void AddCreativeItem(ItemStack item)
        {
            MineNET_Registries.Creative.Add(item);
        }

        public static void RemoveCreativeItem(ItemStack item)
        {
            MineNET_Registries.Creative.Remove(item);
        }

        public static void RemoveCreativeItem(int index)
        {
            MineNET_Registries.Creative.RemoveAt(index);
        }

        public static void AddCreativeItems(params ItemStack[] items)
        {
            for (int i = 0; i < items.Length; ++i)
            {
                Item.AddCreativeItem(items[i]);
            }
        }

        public static void RemoveAllCreativeItems()
        {
            MineNET_Registries.Creative.Clear();
        }

        public static ItemStack[] GetCreativeItems()
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

                Item.AddCreativeItem(new ItemStack(Item.Get(id), damage, 1, nbt));
            }
        }

        public Item(string name, int id)
        {
            this.Name = name;
            this.ID = id;
        }

        public virtual string Name { get; } = "Unknown";

        public virtual byte MaxStackSize
        {
            get
            {
                return 64;
            }
        }

        public virtual bool IsTool
        {
            get
            {
                return false;
            }
        }

        public virtual bool IsPickaxe
        {
            get
            {
                return false;
            }
        }

        public virtual bool IsAxe
        {
            get
            {
                return false;
            }
        }

        public virtual bool IsSword
        {
            get
            {
                return false;
            }
        }

        public virtual bool IsShovel
        {
            get
            {
                return false;
            }
        }

        public virtual bool IsHoe
        {
            get
            {
                return false;
            }
        }

        public virtual bool IsShears
        {
            get
            {
                return false;
            }
        }

        public virtual bool IsFlintAndSteel
        {
            get
            {
                return false;
            }
        }

        public virtual bool IsArmor
        {
            get
            {
                return false;
            }
        }

        public virtual bool IsHemlet
        {
            get
            {
                return false;
            }
        }

        public virtual bool IsChestplate
        {
            get
            {
                return false;
            }
        }

        public virtual bool IsLeggings
        {
            get
            {
                return false;
            }
        }

        public virtual bool IsBoots
        {
            get
            {
                return false;
            }
        }

        public virtual bool Activate(Player player, World world, Block clicked, BlockFace blockFace, Vector3 clickPos)
        {
            return false;
        }

        public virtual bool BlockDestroyed(Block block, EntityLiving entity)
        {
            return false;
        }

        public virtual bool HitEntity(EntityLiving attacker, EntityLiving target)
        {
            return false;
        }

        public Block Block
        {
            get
            {
                if (this.block == null)
                {
                    return Block.Get(BlockIDs.AIR);
                }
                else
                {
                    return this.block.Clone();
                }
            }

            set
            {
                this.block = value;
            }
        }

        public virtual bool CanBeConsumed
        {
            get
            {
                return false;
            }
        }

        public virtual bool CanBePlace
        {
            get
            {
                return this.Block != null && this.Block.CanBePlaced;
            }
        }

        public virtual bool CanBeActivate
        {
            get
            {
                return false;
            }
        }

        public virtual int AttackDamage
        {
            get
            {
                return 1;
            }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Item))
            {
                return false;
            }
            Item item = (Item) obj;
            if (this.ID != item.ID)
            {
                return false;
            }
            return true;
        }

        public static bool operator ==(Item A, Item B)
        {
            if (object.ReferenceEquals(A, B))
            {
                return true;
            }
            if ((object) A == null || (object) B == null)
            {
                return false;
            }
            return A.Equals(B);
        }

        public static bool operator !=(Item A, Item B)
        {
            if (object.ReferenceEquals(A, B))
            {
                return false;
            }
            if ((object) A == null || (object) B == null)
            {
                return true;
            }
            return !A.Equals(B);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
