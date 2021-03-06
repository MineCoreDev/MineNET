﻿namespace MineNET.Items.Enchantments
{
    public class EnchantmentProjectileProtection : Enchantment
    {
        public override int ID
        {
            get
            {
                return Enchantment.PROJECTILE_PROTECTION;
            }
        }

        public override int MinLevel
        {
            get
            {
                return 1;
            }
        }

        public override int MaxLevel
        {
            get
            {
                return 4;
            }
        }

        public override int Weight
        {
            get
            {
                return 5;
            }
        }

        public override string Name
        {
            get
            {
                return "enchantment.projectile_protection";
            }
        }
    }
}
