using MineNET.Utils;

namespace MineNET.Items.Enchantment
{
    public class EnchantmentUnbreaking : Enchantment
    {
        public override int ID
        {
            get
            {
                return Enchantment.UNBREAKING;
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
                return 5;
            }
        }

        public override string Name
        {
            get
            {
                return LangManager.GetString("enchantment.unbreaking");
            }
        }
    }
}
