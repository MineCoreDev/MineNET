using System;
using MineNET.Blocks;
using MineNET.Entities;
using MineNET.Entities.Players;
using MineNET.Items.Enchantments;

namespace MineNET.Items
{
    public abstract class ItemTool : Item
    {
        public ItemTool(int id) : base(id)
        {

        }

        abstract public int MaxDurability { get; }

        public override bool BlockDestroyed(Block block, EntityLiving entity)
        {
            if (entity.IsPlayer && ((Player) entity).IsCreative)
            {
                return false;
            }
            if (this.Unbreakable)
            {
                return false;
            }
            int level = 0;
            if (this.HasEnchantment(Enchantment.UNBREAKING))
            {
                level = this.GetEnchantment(Enchantment.UNBREAKING).Level;
            }
            float percent = (1f / (1f + level)) * 10000f;
            if (new Random().Next(0, 10000) > percent)
            {
                return false;
            }
            this.Damage++;
            if (this.Damage >= this.MaxDurability)
            {
                entity.Inventory.MainHandItem = Item.Get(0);
                //TODO : sound
            }
            else
            {
                entity.Inventory.MainHandItem = this;
            }
            return true;
        }

        public override bool HitEntity(EntityLiving attacker, EntityLiving target)
        {
            if (this.Unbreakable)
            {
                return false;
            }
            if (!this.IsSword)
            {

            }
            return true;
        }

        public override bool IsTool
        {
            get
            {
                return true;
            }
        }

        public override byte MaxStackSize
        {
            get
            {
                return 1;
            }
        }
    }
}
