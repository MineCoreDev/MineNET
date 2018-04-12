﻿using MineNET.Utils;

namespace MineNET.Items.Enchantment
{
    public class EnchantmentEfficiency : Enchantment
    {
        public override int ID
        {
            get
            {
                return Enchantment.EFFICIENCY;
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
                return LangManager.GetString("enchantment.efficiency");
            }
        }
    }
}
