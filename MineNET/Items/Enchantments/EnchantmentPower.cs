﻿namespace MineNET.Items.Enchantments
{
    public class EnchantmentPower : Enchantment
    {
        public override int ID
        {
            get
            {
                return Enchantment.POWER;
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
                return 5;
            }
        }

        public override int Weight
        {
            get
            {
                return 10;
            }
        }

        public override string Name
        {
            get
            {
                return "enchantment.power";
            }
        }
    }
}
