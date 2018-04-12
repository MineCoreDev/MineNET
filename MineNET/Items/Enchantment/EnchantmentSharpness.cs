using MineNET.Utils;

namespace MineNET.Items.Enchantment
{
    public class EnchantmentSharpness : Enchantment
    {
        public override int ID
        {
            get
            {
                return Enchantment.SHARPNESS;
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
                return 10;
            }
        }

        public override string Name
        {
            get
            {
                return LangManager.GetString("enchantment.sharpness");
            }
        }
    }
}
