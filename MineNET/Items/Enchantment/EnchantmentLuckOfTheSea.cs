using MineNET.Utils;

namespace MineNET.Items.Enchantment
{
    public class EnchantmentLuckOfTheSea : Enchantment
    {
        public override int ID
        {
            get
            {
                return Enchantment.LUCK_OF_THE_SEA;
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
                return 3;
            }
        }

        public override int Weight
        {
            get
            {
                return 2;
            }
        }

        public override string Name
        {
            get
            {
                return LangManager.GetString("enchantment.luck_of_the_sea");
            }
        }
    }
}
