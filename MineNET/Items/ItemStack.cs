using System;
using System.Collections;
using MineNET.Blocks;
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

        public void AddCanPlaceOn(string v)
        {
            throw new NotImplementedException();
        }

        public void AddCanDestroy(string v)
        {
            throw new NotImplementedException();
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

        public override bool Equals(object obj)
        {
            return this.Equals(obj);
        }

        public bool Equals(object obj, bool checkDamage = true, bool checkCount = true, bool checkNBT = true,
            bool checkComponents = true)
        {
            if (!(obj is ItemStack))
            {
                return false;
            }

            ItemStack stack = (ItemStack) obj;
            if (!this.Item.Equals(stack))
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
                if (!((IStructuralEquatable) this.CanPlaceOn).Equals(stack.CanPlaceOn,
                        StructuralComparisons.StructuralEqualityComparer) ||
                    !((IStructuralEquatable) this.CanDestroy).Equals(stack.CanDestroy,
                        StructuralComparisons.StructuralEqualityComparer))
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