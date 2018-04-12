using MineNET.Utils;

namespace MineNET.Items.Enchantment
{
    public class EnchantmentInfinity : Enchantment
    {
        public override int ID
        {
            get
            {
                return Enchantment.INFINITY;
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
                return 1;
            }
        }

        public override int Weight
        {
            get
            {
                return 1;
            }
        }

        public override string Name
        {
            get
            {
                return LangManager.GetString("enchantment.infinity");
            }
        }
    }
}
