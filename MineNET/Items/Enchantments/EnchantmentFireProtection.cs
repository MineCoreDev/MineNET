using MineNET.Utils;

namespace MineNET.Items.Enchantments
{
    public class EnchantmentFireProtection : Enchantment
    {
        public override int ID
        {
            get
            {
                return Enchantment.FIRE_PROTECTION;
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
                return LangManager.GetString("enchantment.fire_protection");
            }
        }
    }
}
