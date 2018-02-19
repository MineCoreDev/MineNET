using System;
using System.Reflection;
using MineNET.Blocks;
using MineNET.NBT.Tags;
using MineNET.Utils;

namespace MineNET.Items
{
    public class Item : ICloneable<Item>
    {
        public static Item Get(int id, short meta = 0, byte count = 1, byte[] tags = null)
        {
            Item item = ItemFactory.GetItem(id);
            item.Damage = meta;
            item.Count = count;
            if (tags == null)
            {
                tags = new byte[0];
            }
            item.tags = tags;
            return item;
        }

        public static Item Get(string name)
        {
            string[] data = name.Replace("minecraft:", "").Replace(" ", "_").ToUpper().Split(':');
            int id = 0;
            int meta = 0;

            if (data.Length == 1)
            {
                int.TryParse(data[0], out id);
            }

            if (data.Length == 2)
            {
                int.TryParse(data[0], out id);
                int.TryParse(data[1], out meta);
            }

            try
            {
                ItemFactory factory = new ItemFactory();
                FieldInfo info = factory.GetType().GetField(data[0]);
                id = (int) info.GetValue(factory);
            }
            catch
            {
                try
                {
                    BlockFactory factory = new BlockFactory();
                    FieldInfo info = factory.GetType().GetField(data[0]);
                    id = (int) info.GetValue(factory);
                }
                catch
                {

                }
            }

            Item item = Item.Get(id, (short) meta);
            return item;
        }

        public Item Clone()
        {
            return (Item) Clone();
        }

        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }

        private int id;
        private short meta;
        private byte count;
        private byte[] tags = new byte[0];
        private CompoundTag cachedNBT = null;

        private Block block = null;

        public Item(int id, short meta = 0, byte count = 1)
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

        public bool HasTag()
        {
            return this.tags != null && this.tags.Length > 0;
        }

        public byte[] Tag
        {
            get
            {
                return this.tags;
            }

            set
            {
                this.tags = value;
                this.cachedNBT = null;
            }
        }

        public CompoundTag GetNamedTag()
        {
            if (!this.HasTag())
            {
                return new CompoundTag();
            }
            //TODO : this.tag -> CompoundTag
            return new CompoundTag();
        }

        public Item SetNamedTag(CompoundTag nbt)
        {
            nbt.Name = null;
            this.cachedNBT = nbt;
            //this.tags = nbt;
            return this;
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
