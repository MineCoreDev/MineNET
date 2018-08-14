using MineNET.Blocks;

namespace MineNET.Items
{
    public class Item
    {
        public int ID { get; }
        public int Damage { get; }

        public static Item Get(int id)
        {
            if (MineNET_Registries.Item.ContainsKey(id))
            {
                if (id <= 0xff)
                {
                    return new ItemBlock(Block.Get(id));
                }
                else
                {
                    return MineNET_Registries.Item[id];
                }
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

            try
            {
                ItemIDs factory = new ItemIDs();
                id = (int) factory.GetType().GetField(data[0]).GetValue(factory);
            }
            catch
            {
                try
                {
                    BlockIDs factory = new BlockIDs();
                    id = (int) factory.GetType().GetField(data[0]).GetValue(factory);
                }
                catch
                {

                }
            }

            Item item = Item.Get(id);
            return item;
        }

        public Item(string name, int id)
        {
            this.Name = name;
            this.ID = id;
        }

        public Item(string name, int id, int damage)
        {
            this.Name = name;
            this.ID = id;
            this.Damage = damage;
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

        public override bool Equals(object obj)
        {
            return this.Equals(obj);
        }

        public bool Equals(object obj, bool checkDamage = true)
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
            if (checkDamage)
            {
                if (this.Damage != item.Damage)
                {
                    return false;
                }
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
