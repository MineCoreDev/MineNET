using MineNET.Utils;

namespace MineNET.Items.Enchantment
{
    public class EnchantmentKnockback : Enchantment
    {
        public override int ID
        {
            get
            {
                return Enchantment.KNOCKBACK;
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
                return 2;
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
                return LangManager.GetString("enchantment.knockback");
            }
        }
    }
}
