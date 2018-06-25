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
                return new ItemBlock(Init.BlockInit.In.Air);
            }
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
    }
}
