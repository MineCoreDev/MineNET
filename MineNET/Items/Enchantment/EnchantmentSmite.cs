using MineNET.Utils;

namespace MineNET.Items.Enchantment
{
    public class EnchantmentSmite : Enchantment
    {
        public override int ID
        {
            get
            {
                return Enchantment.SMITE;
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
                return 5;
            }
        }

        public override string Name
        {
            get
            {
                return LangManager.GetString("enchantment.smite");
            }
        }
    }
}
