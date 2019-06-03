using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MineNET.Blocks;
using MineNET.Items.Enchantments;
using MineNET.NBT.Data;
using MineNET.NBT.IO;
using MineNET.NBT.Tags;

namespace MineNET.Items
{
    public struct ItemStack
    {
        public Item Item { get; }
        public int Damage { get; set; }
        public int Count { get; set; }

        public string[] CanPlaceOn { get; private set; }
        public string[] CanDestroy { get; private set; }

        public CompoundTag NamedTag { get; set; }

        #region Constructor

        public ItemStack(Item item) : this(item, 0)
        {

        }

        public ItemStack(Item item, int damage) : this(item, damage, 1)
        {

        }

        public ItemStack(Item item, int damage, int count) : this(item, damage, count, new CompoundTag())
        {

        }

        public ItemStack(Item item, int damage, int count, byte[] nbt) : this(item, damage, count, NBTIO.ReadTag(nbt))
        {

        }

        public ItemStack(Item item, int damage, int count, CompoundTag tag)
        {
            this.Item = item;
            this.Damage = 0;
            this.Count = 1;

            this.CanPlaceOn = new string[0];
            this.CanDestroy = new string[0];

            if (tag == null)
            {
                tag = new CompoundTag();
            }
            this.NamedTag = tag;
        }

        public ItemStack(Block block) : this(block, block.Damage)
        {

        }

        public ItemStack(Block block, int damage) : this(block, damage, 1)
        {

        }

        public ItemStack(Block block, int damage, int count) : this(block, damage, count, new CompoundTag())
        {

        }

        public ItemStack(Block block, int damage, int count, byte[] nbt) : this(block, damage, count, NBTIO.ReadTag(nbt))
        {

        }

        public ItemStack(Block block, int damage, int count, CompoundTag tag)
        {
            this.Item = new ItemBlock(block);
            this.Damage = damage;
            this.Count = count;

            this.CanPlaceOn = new string[0];
            this.CanDestroy = new string[0];

            if (tag == null)
            {
                tag = new CompoundTag();
            }
            this.NamedTag = tag;
        }

        #endregion

        public int ID
        {
            get
            {
                return this.Item.ID;
            }
        }

        public string Name
        {
            get
            {
                return this.Item.GetName(this.Damage);
            }
        }

        #region Components

        public ItemStack AddCanPlaceOn(params string[] blocks)
        {
            List<string> canPlaceOn = this.CanPlaceOn.ToList();
            for (int i = 0; i < blocks.Length; ++i)
            {
                canPlaceOn.Add(blocks[i]);
            }
            this.CanPlaceOn = canPlaceOn.ToArray();
            return this;
        }

        public ItemStack RemoveCanPlaceOn(params string[] blocks)
        {
            List<string> canPlaceOn = this.CanPlaceOn.ToList();
            for (int i = 0; i < blocks.Length; ++i)
            {
                if (canPlaceOn.Contains(blocks[i]))
                {
                    canPlaceOn.Remove(blocks[i]);
                }
            }
            this.CanPlaceOn = canPlaceOn.ToArray();
            return this;
        }

        public ItemStack AddCanDestroy(params string[] blocks)
        {
            List<string> canDestroy = this.CanDestroy.ToList();
            for (int i = 0; i < blocks.Length; ++i)
            {
                canDestroy.Add(blocks[i]);
            }
            this.CanDestroy = canDestroy.ToArray();
            return this;
        }

        public ItemStack RemoveCanDestroy(params string[] blocks)
        {
            List<string> canDestroy = this.CanDestroy.ToList();
            for (int i = 0; i < blocks.Length; ++i)
            {
                if (canDestroy.Contains(blocks[i]))
                {
                    canDestroy.Remove(blocks[i]);
                }
            }
            this.CanDestroy = canDestroy.ToArray();
            return this;
        }

        #endregion

        #region NBT CustomName

        public string GetCustomName()
        {
            CompoundTag tag = this.NamedTag;
            if (!tag.Exist("display"))
            {
                return "";
            }

            CompoundTag display = tag.GetCompound("display");
            if (!display.Exist("Name"))
            {
                return "";
            }
            return display.GetString("Name");
        }

        public ItemStack SetCustomName(string name)
        {
            if (name == null || name == "")
            {
                this.ClearCustomName();
                return this;
            }

            CompoundTag tag = this.NamedTag;
            if (tag.Exist("display"))
            {
                tag.GetCompound("display").PutString("Name", name);
            }
            else
            {
                tag.PutCompound("display", new CompoundTag("display").PutString("Name", name));
            }
            this.NamedTag = tag;
            return this;
        }

        public ItemStack ClearCustomName()
        {
            CompoundTag tag = this.NamedTag;
            if (!tag.Exist("display"))
            {
                return this;
            }

            CompoundTag display = tag.GetCompound("display");
            if (display.Exist("Name"))
            {
                display.Remove("Name");
            }
            this.NamedTag = tag;
            return this;
        }

        #endregion

        #region NBT Lore

        public string[] GetLore()
        {
            CompoundTag tag = this.NamedTag;
            if (tag.Exist("display"))
            {
                return new string[0];
            }

            CompoundTag display = tag.GetCompound("display");
            if (!display.Exist("Lore"))
            {
                return new string[0];
            }

            ListTag lores = display.GetList("Lore");
            string[] data = new string[lores.Count];
            for (int i = 0; i < lores.Count; ++i)
            {
                data[i] = ((StringTag) lores[i]).Data;
            }
            return data;
        }

        public ItemStack SetLore(params string[] lores)
        {
            if (lores == null || lores.Length < 1)
            {
                this.ClearLore();
                return this;
            }

            CompoundTag tag = this.NamedTag;
            ListTag list = new ListTag("Lore", NBTTagType.STRING);
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
            this.NamedTag = tag;
            return this;
        }

        public ItemStack AddLore(params string[] lores)
        {
            if (lores == null || lores.Length < 1)
            {
                return this;
            }
            if (this.GetLore().Length < 1)
            {
                this.SetLore(lores);
                return this;
            }

            CompoundTag tag = this.NamedTag;
            ListTag list = tag.GetCompound("display").GetList("Lore");
            for (int i = 0; i < lores.Length; ++i)
            {
                list.Add(new StringTag(lores[i]));
            }
            this.NamedTag = tag;
            return this;
        }

        public ItemStack ClearLore()
        {
            CompoundTag tag = this.NamedTag;
            if (!tag.Exist("display"))
            {
                return this;
            }

            CompoundTag display = tag.GetCompound("display");
            if (display.Exist("Lore"))
            {
                display.Remove("Lore");
            }
            this.NamedTag = tag;
            return this;
        }

        #endregion

        #region NBT Unbreakable

        public bool GetUnbreakable()
        {
            CompoundTag tag = this.NamedTag;
            if (!tag.Exist("Unbreakable"))
            {
                return false;
            }
            return tag.GetBool("Unbreakable");
        }

        public ItemStack SetUnbreakable(bool value)
        {
            CompoundTag tag = this.NamedTag;
            tag.PutBool("Unbreakable", value);
            this.NamedTag = tag;
            return this;
        }

        #endregion

        #region NBT Enchantment

        public bool HasEnchantment(int id)
        {
            CompoundTag tag = this.NamedTag;
            if (!tag.Exist("ench"))
            {
                return false;
            }

            ListTag list = tag.GetList("ench");
            for (int i = 0; i < list.Count; ++i)
            {
                CompoundTag ench = (CompoundTag) list[i];
                if (ench.GetShort("id") == id)
                {
                    return true;
                }
            }
            return false;
        }

        public Enchantment GetEnchantment(int id)
        {
            if (!this.HasEnchantment(id))
            {
                return null;
            }

            CompoundTag tag = this.NamedTag;
            ListTag list = tag.GetList("ench");
            for (int i = 0; i < list.Count; ++i)
            {
                CompoundTag ench = (CompoundTag) list[i];
                if (ench.GetShort("id") == id)
                {
                    return Enchantment.GetEnchantment(id, ench.GetShort("lvl"));
                }
            }
            return null;
        }

        public Enchantment[] GetEnchantments()
        {
            CompoundTag tag = this.NamedTag;
            if (!tag.Exist("ench"))
            {
                return new Enchantment[0];
            }

            ListTag list = tag.GetList("ench");
            List<Enchantment> enches = new List<Enchantment>();
            for (int i = 0; i < list.Count; ++i)
            {
                CompoundTag ench = (CompoundTag) list[i];
                enches.Add(Enchantment.GetEnchantment(ench.GetShort("id"), ench.GetShort("lvl")));
            }
            return enches.ToArray();
        }

        public ItemStack AddEnchantment(Enchantment enchantment)
        {
            CompoundTag tag = this.NamedTag;
            ListTag list;
            if (tag.Exist("ench"))
            {
                list = tag.GetList("ench");
            }
            else
            {
                list = new ListTag("ench", NBTTagType.COMPOUND);
                tag.PutList(list);
            }
            for (int i = 0; i < list.Count; ++i)
            {
                if (((CompoundTag) list[i]).GetShort("id") == enchantment.ID)
                {
                    list[i] = new CompoundTag()
                        .PutShort("id", (short) enchantment.ID)
                        .PutShort("lvl", (short) enchantment.Level);
                    this.NamedTag = tag;
                    return this;
                }
            }
            CompoundTag ench = new CompoundTag()
                        .PutShort("id", (short) enchantment.ID)
                        .PutShort("lvl", (short) enchantment.Level);
            list.Add(ench);
            this.NamedTag = tag;
            return this;
        }

        public ItemStack RemoveEnchantment(Enchantment enchantment)
        {
            CompoundTag tag = this.NamedTag;
            if (!tag.Exist("ench"))
            {
                return this;
            }
            ListTag list = tag.GetList("ench");
            for (int i = 0; i < list.Count; ++i)
            {
                if (((CompoundTag) list[i]).GetShort("id") == enchantment.ID)
                {
                    list.RemoveAt(i);
                }
            }
            this.NamedTag = tag;
            return this;
        }

        public ItemStack RemoveEnchantments()
        {
            CompoundTag tag = this.NamedTag;
            if (tag.Exist("ench"))
            {
                tag.Remove("ench");
            }
            return this;
        }

        #endregion

        public override bool Equals(object obj)
        {
            return this.Equals(obj);
        }

        public bool Equals(object obj, bool checkDamage = true, bool checkCount = true, bool checkNBT = true, bool checkComponents = true)
        {
            if (!(obj is ItemStack))
            {
                return false;
            }

            ItemStack stack = (ItemStack) obj;
            if (!this.Item.Equals(stack.Item))
            {
                return false;
            }

            if (checkDamage && this.Damage != stack.Damage)
            {
                return false;
            }

            if (checkCount && this.Count != stack.Count)
            {
                return false;
            }

            if (checkNBT && this.NamedTag != stack.NamedTag)
            {
                return false;
            }

            if (checkComponents)
            {
                if (!((IStructuralEquatable) this.CanPlaceOn).Equals(stack.CanPlaceOn, StructuralComparisons.StructuralEqualityComparer) ||
                    !((IStructuralEquatable) this.CanDestroy).Equals(stack.CanDestroy, StructuralComparisons.StructuralEqualityComparer))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool operator ==(ItemStack A, ItemStack B)
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

        public static bool operator !=(ItemStack A, ItemStack B)
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