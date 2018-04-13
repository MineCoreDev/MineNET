using MineNET.Utils;

namespace MineNET.Items.Enchantments
{
    public class EnchantmentProtection : Enchantment
    {
        public override int ID
        {
            get
            {
                return Enchantment.PROTECTION;
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
                return 10;
            }
        }

        public override string Name
        {
            get
            {
                return LangManager.GetString("enchantment.protection");
            }
        }
    }
}
