using MineNET.Utils;

namespace MineNET.Items.Enchantment
{
    public class EnchantmentAquaAffinity : Enchantment
    {
        public override int ID
        {
            get
            {
                return Enchantment.AQUA_AFFINITY;
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
                return 2;
            }
        }

        public override string Name
        {
            get
            {
                return LangManager.GetString("enchantment.aqua_affinity");
            }
        }
    }
}
