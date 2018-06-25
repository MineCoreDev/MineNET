using MineNET.Blocks;
using MineNET.NBT.Tags;
using System;

namespace MineNET.Items
{
    public class ItemStack : ICloneable<ItemStack>
    {
        public Item Item { get; }
        public int Damage { get; set; } = 0;
        public int Count { get; set; } = 1;

        public CompoundTag Tags { get; set; } = null;
        public byte[] BinaryTags { get; } = null;

        public string[] CanPlaceOn { get; private set; } = new string[0];
        public string[] CanDestroy { get; private set; } = new string[0];

        public ItemStack(Item item)
        {
            this.Item = item;
            this.Damage = item.Damage;
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
            this.BinaryTags = nbt;
        }

        public ItemStack(Item item, int damage, int count, CompoundTag tag) : this(item, damage, count)
        {
            this.Tags = tag;
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
            this.BinaryTags = nbt;
        }

        public ItemStack(Block block, int damage, int count, CompoundTag tag) : this(block, damage, count)
        {
            this.Tags = tag;
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
    }
}
