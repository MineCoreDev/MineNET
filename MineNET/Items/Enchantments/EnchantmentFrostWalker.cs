using MineNET.Utils;

namespace MineNET.Items.Enchantments
{
    public class EnchantmentFrostWalker : Enchantment
    {
        public override int ID
        {
            get
            {
                return Enchantment.FROST_WALKER;
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
                return 2;
            }
        }

        public override string Name
        {
            get
            {
                return LangManager.GetString("enchantment.frost_walker");
            }
        }
    }
}
