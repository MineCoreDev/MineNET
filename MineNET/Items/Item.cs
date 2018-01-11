using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MineNET.Blocks;

namespace MineNET.Items
{
    public class Item : ICloneable
    {
        public static Item Get(int id)
        {
            Item item = ItemFactory.GetItem(id);
            return item;
        }

        public static Item Get(int id, short meta)
        {
            Item item = Get(id);
            item.Damage = meta;
            return item;
        }

        public static Item Get(int id, short meta, byte count)
        {
            Item item = Get(id);
            item.Damage = meta;
            item.Count = count;
            return item;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        private int id;
        private short meta;
        private byte count;

        private Block block = null;

        public Item(int id) : this(id, 0)
        {

        }

        public Item(int id, short meta) : this(id, meta, 1)
        {

        }

        public Item(int id, short meta, byte count)
        {
            this.id = id;
            this.meta = meta;
            this.count = count;
        }

        public virtual string Name
        {
            get
            {
                return "Unknown";
            }
        }

        public int ItemID
        {
            get
            {
                return this.id;
            }
        }

        public short Damage
        {
            get
            {
                return this.meta;
            }

            set
            {
                this.meta = value;
            }
        }

        public byte Count
        {
            get
            {
                return this.count;
            }

            set
            {
                this.count = value;
            }
        }

        public Block Block
        {
            get
            {
                if (this.block == null)
                {
                    return Block.Get(BlockFactory.AIR);
                }
                else
                {
                    return this.block;
                }
            }

            set
            {
                this.block = value;
            }
        }

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

        public virtual bool IsArmor
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

        public virtual bool CanBeConsumed
        {
            get
            {
                return false;
            }
        }

        public virtual int FoodRestore
        {
            get
            {
                return 0;
            }
        }

        public virtual float SaturationRestore
        {
            get
            {
                return 0f;
            }
        }
    }
}
