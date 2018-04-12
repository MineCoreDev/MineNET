using MineNET.Utils;

namespace MineNET.Items.Enchantment
{
    public class EnchantmentLooting : Enchantment
    {
        public override int ID
        {
            get
            {
                return Enchantment.LOOTING;
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
                return LangManager.GetString("enchantment.looting");
            }
        }
    }
}
