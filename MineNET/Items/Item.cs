using System;
using MineNET.Blocks;
using MineNET.NBT;
using MineNET.NBT.Tags;
using MineNET.Utils;

namespace MineNET.Items
{
    public class Item : ICloneable<Item>
    {
        public static Item Get(int id, int meta = 0, int count = 1, byte[] tags = null)
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
            return ItemFactory.GetItem(name);
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
        private int meta;
        private int count;
        private byte[] tags = new byte[0];
        private CompoundTag cachedNBT = null;

        private Block block = null;

        public Item(int id, int meta = 0, int count = 1)
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

        public int Damage
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

        public int Count
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

        public bool HasTags()
        {
            return this.tags != null && this.tags.Length > 0;
        }

        public byte[] Tags
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
            if (!this.HasTags())
            {
                return new CompoundTag();
            }

            if (this.cachedNBT == null)
            {
                this.cachedNBT = NBTIO.ReadTag(this.tags);
            }

            if (this.cachedNBT != null)
            {
                this.cachedNBT.Name = "";
            }
            return this.cachedNBT;
        }

        public Item SetNamedTag(CompoundTag nbt)
        {
            nbt.Name = null;
            this.cachedNBT = nbt;
            this.tags = NBTIO.WriteTag(nbt);
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

        public string GetCustomName()
        {
            if (!this.HasTags())
            {
                return "";
            }

            CompoundTag tag = this.GetNamedTag();
            if (tag.Exist("display"))
            {
                return "";
            }
            CompoundTag display = tag.GetCompound("display");
            if (!display.Exist("name"))
            {
                return "";
            }
            return display.GetString("name");
        }

        public Item SetCustomName(string name)
        {
            if (name == null || name == "")
            {
                this.ClearCustomName();
            }
            else
            {
                CompoundTag tag;
                if (this.HasTags())
                {
                    tag = this.GetNamedTag();
                }
                else
                {
                    tag = new CompoundTag();
                }
                if (tag.Exist("display"))
                {
                    tag.GetCompound("display").PutString("name", name);
                }
                else
                {
                    tag.PutCompound("display", new CompoundTag("display").PutString("name", name));
                }
                this.SetNamedTag(tag);
            }
            return this;
        }

        public Item ClearCustomName()
        {
            if (!this.HasTags())
            {
                return this;
            }

            CompoundTag tag = this.GetNamedTag();
            if (!tag.Exist("display"))
            {
                return this;
            }
            CompoundTag display = tag.GetCompound("display");
            if (display.Exist("name"))
            {
                display.Remove("name");
            }
            this.SetNamedTag(tag);
            return this;
        }

        public string[] GetLore()
        {
            if (!this.HasTags())
            {
                return new string[0];
            }

            CompoundTag tag = this.GetNamedTag();
            if (tag.Exist("display"))
            {
                return new string[0];
            }
            CompoundTag display = tag.GetCompound("display");
            if (!display.Exist("lore"))
            {
                return new string[0];
            }
            ListTag<StringTag> lores = display.GetList<StringTag>("lore");
            string[] data = new string[lores.Count];
            for (int i = 0; i < lores.Count; ++i)
            {
                data[i] = lores[i].Data;
            }
            return data;
        }

        public Item SetLore(params string[] lores)
        {
            if (lores == null || lores.Length < 1)
            {
                this.ClearLore();
            }
            else
            {
                CompoundTag tag;
                if (this.HasTags())
                {
                    tag = this.GetNamedTag();
                }
                else
                {
                    tag = new CompoundTag();
                }
                ListTag<StringTag> list = new ListTag<StringTag>("lore");
                for (int i = 0; i < lores.Length; ++i)
                {
                    list.Add(new StringTag(lores[i]));
                }
                if (tag.Exist("display"))
                {
                    tag.GetCompound("display").PutList(list);
                }
                else
                {
                    tag.PutCompound("display", new CompoundTag("display").PutList(list));
                }
                this.SetNamedTag(tag);
            }
            return this;
        }

        public Item AddLore(params string[] lores)
        {
            if (lores == null || lores.Length < 1)
            {
                return this;
            }
            if (!this.HasTags() || this.GetLore().Length < 1)
            {
                this.SetLore(lores);
                return this;
            }
            CompoundTag tag = this.GetNamedTag();
            ListTag<StringTag> list = tag.GetCompound("display").GetList<StringTag>("lore");
            for (int i = 0; i < lores.Length; ++i)
            {
                list.Add(new StringTag(lores[i]));
            }
            this.SetNamedTag(tag);
            return this;
        }

        public Item ClearLore()
        {
            if (!this.HasTags())
            {
                return this;
            }

            CompoundTag tag = this.GetNamedTag();
            if (!tag.Exist("display"))
            {
                return this;
            }
            CompoundTag display = tag.GetCompound("display");
            if (display.Exist("lore"))
            {
                display.Remove("lore");
            }
            this.SetNamedTag(tag);
            return this;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Item))
            {
                return false;
            }
            Item item = (Item) obj;
            if (this.id != item.id || this.meta != item.meta || this.count != item.count)
            {
                return false;
            }
            if (this.HasTags() != item.HasTags())
            {
                return false;
            }
            else
            {
                if (this.HasTags())
                {
                    if (this.GetNamedTag() != item.GetNamedTag())
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool operator ==(Item A, Item B)
        {
            return A.Equals(B);
        }

        public static bool operator !=(Item A, Item B)
        {
            return !A.Equals(B);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
