using System;
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
    public class ItemStack : ICloneable<ItemStack>
    {
        public Item Item { get; }
        public int Damage { get; set; } = 0;
        public int Count { get; set; } = 1;

        public byte[] BinaryTags { get; private set; } = new byte[0];

        public bool HasTags
        {
            get
            {
                if (this.BinaryTags != null)
                {
                    return this.BinaryTags.Length > 0;
                }
                else
                {
                    return false;
                }
            }
        }

        public string[] CanPlaceOn { get; private set; } = new string[0];
        public string[] CanDestroy { get; private set; } = new string[0];

        private CompoundTag Tags { get; set; } = null;

        public ItemStack(Item item)
        {
            this.Item = item;
        }

        public ItemStack(Item item, int damage) : this(item)
        {
            this.Damage = damage;
        }

        public ItemStack(Item item, int damage, int count) : this(item, damage)
        {
            this.Count = count;
        }

        public ItemStack(Item item, int damage, int count, byte[] nbt) : this(item, damage, count)
        {
            if (nbt != null)
            {
                this.BinaryTags = nbt;
            }
        }

        public ItemStack(Item item, int damage, int count, CompoundTag tag) : this(item, damage, count)
        {
            if (tag != null)
            {
                this.Tags = tag;
            }
        }

        public ItemStack(Block block)
        {
            this.Item = new ItemBlock(block);
            this.Damage = block.Damage;
        }

        public ItemStack(Block block, int damage) : this(block)
        {
            this.Damage = damage;
        }

        public ItemStack(Block block, int damage, int count) : this(block, damage)
        {
            this.Count = count;
        }

        public ItemStack(Block block, int damage, int count, byte[] nbt) : this(block, damage, count)
        {
            if (nbt != null)
            {
                this.BinaryTags = nbt;
            }
        }

        public ItemStack(Block block, int damage, int count, CompoundTag tag) : this(block, damage, count)
        {
            if (tag != null)
            {
                this.Tags = tag;
            }
        }

        public ItemStack Clone()
        {
            return (ItemStack) this.MemberwiseClone();
        }

        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }

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

        public void SetNamedTag(CompoundTag tag)
        {
            tag.Name = "";
            this.Tags = tag;
            this.BinaryTags = NBTIO.WriteTag(tag);
        }

        public CompoundTag GetNamedTag()
        {
            if (!this.HasTags)
            {
                return new CompoundTag();
            }

            if (this.Tags == null)
            {
                this.Tags = NBTIO.ReadTag(this.BinaryTags);
            }

            if (this.Tags != null)
            {
                this.Tags.Name = "";
            }

            return this.Tags;
        }

        #region NBT CustomName

        public string GetCustomName()
        {
            if (!this.HasTags)
            {
                return "";
            }

            CompoundTag tag = this.GetNamedTag();
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
            }
            else
            {
                CompoundTag tag;
                if (this.HasTags)
                {
                    tag = this.GetNamedTag();
                }
                else
                {
                    tag = new CompoundTag();
                }
                if (tag.Exist("display"))
                {
                    tag.GetCompound("display").PutString("Name", name);
                }
                else
                {
                    tag.PutCompound("display", new CompoundTag("display").PutString("Name", name));
                }
                this.SetNamedTag(tag);
            }
            return this;
        }

        public ItemStack ClearCustomName()
        {
            if (!this.HasTags)
            {
                return this;
            }

            CompoundTag tag = this.GetNamedTag();
            if (!tag.Exist("display"))
            {
                return this;
            }
            CompoundTag display = tag.GetCompound("display");
            if (display.Exist("Name"))
            {
                display.Remove("Name");
            }
            this.SetNamedTag(tag);
            return this;
        }

        #endregion

        #region NBT Lore

        public string[] GetLore()
        {
            if (!this.HasTags)
            {
                return new string[0];
            }

            CompoundTag tag = this.GetNamedTag();
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
            }
            else
            {
                CompoundTag tag;
                if (this.HasTags)
                {
                    tag = this.GetNamedTag();
                }
                else
                {
                    tag = new CompoundTag();
                }
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
                this.SetNamedTag(tag);
            }
            return this;
        }

        public ItemStack AddLore(params string[] lores)
        {
            if (lores == null || lores.Length < 1)
            {
                return this;
            }
            if (!this.HasTags || this.GetLore().Length < 1)
            {
                this.SetLore(lores);
                return this;
            }
            CompoundTag tag = this.GetNamedTag();
            ListTag list = tag.GetCompound("display").GetList("Lore");
            for (int i = 0; i < lores.Length; ++i)
            {
                list.Add(new StringTag(lores[i]));
            }
            this.SetNamedTag(tag);
            return this;
        }

        public ItemStack ClearLore()
        {
            if (!this.HasTags)
            {
                return this;
            }

            CompoundTag tag = this.GetNamedTag();
            if (!tag.Exist("display"))
            {
                return this;
            }
            CompoundTag display = tag.GetCompound("display");
            if (display.Exist("Lore"))
            {
                display.Remove("Lore");
            }
            this.SetNamedTag(tag);
            return this;
        }

        #endregion

        #region NBT Unbreakable

        public bool GetUnbreakable()
        {
            if (!this.HasTags)
            {
                return false;
            }

            CompoundTag tag = this.GetNamedTag();
            if (!tag.Exist("Unbreakable"))
            {
                return false;
            }
            return tag.GetBool("Unbreakable");
        }

        public void SetUnbreakable(bool value)
        {
            CompoundTag tag;
            if (this.HasTags)
            {
                tag = this.GetNamedTag();
            }
            else
            {
                tag = new CompoundTag();
            }
            tag.PutBool("Unbreakable", value);
            this.SetNamedTag(tag);
        }

        #endregion

        #region NBT Enchantment

        public bool HasEnchantment(int id)
        {
            if (!this.HasTags)
            {
                return false;
            }
            CompoundTag tag = this.GetNamedTag();
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
            CompoundTag tag = this.GetNamedTag();
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
            if (!this.HasTags)
            {
                return new Enchantment[0];
            }
            CompoundTag tag = this.GetNamedTag();
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
            CompoundTag tag;
            if (this.HasTags)
            {
                tag = this.GetNamedTag();
            }
            else
            {
                tag = new CompoundTag();
            }
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
                    this.SetNamedTag(tag);
                    return this;
                }
            }
            CompoundTag ench = new CompoundTag()
                        .PutShort("id", (short) enchantment.ID)
                        .PutShort("lvl", (short) enchantment.Level);
            list.Add(ench);
            this.SetNamedTag(tag);
            return this;
        }

        public ItemStack RemoveEnchantment(Enchantment enchantment)
        {
            if (!this.HasTags)
            {
                return this;
            }
            CompoundTag tag = this.GetNamedTag();
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
            this.SetNamedTag(tag);
            return this;
        }

        public ItemStack RemoveEnchantments()
        {
            if (!this.HasTags)
            {
                return this;
            }
            CompoundTag tag = this.GetNamedTag();
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

            if (checkNBT)
            {
                if (this.HasTags != stack.HasTags)
                {
                    return false;
                }
                else
                {
                    if (this.HasTags && this.GetNamedTag() != stack.GetNamedTag())
                    {
                        return false;
                    }
                }
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