using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MineNET.Blocks;

namespace MineNET.Items
{
    public abstract class Item
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

        private string name = "";

        private int id = 0;
        private short meta = 0;
        private byte count = 1;

        private Block block = null;

        public Item(int id)
        {
            this.id = id;
        }

        public Item(int id, short meta) : this(id)
        {
            this.meta = meta;
        }

        public Item(int id, short meta, byte count) : this(id, meta)
        {
            this.count = count;
        }

        public abstract string Name
        {
            get;
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
    }
}
