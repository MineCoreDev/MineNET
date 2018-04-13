using MineNET.Utils;

namespace MineNET.Items.Enchantments
{
    public class EnchantmentThorns : Enchantment
    {
        public override int ID
        {
            get
            {
                return Enchantment.THORNS;
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
                return 1;
            }
        }

        public override string Name
        {
            get
            {
                return LangManager.GetString("enchantment.thorns");
            }
        }
    }
}
