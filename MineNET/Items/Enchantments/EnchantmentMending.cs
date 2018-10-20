namespace MineNET.Items.Enchantments
{
    public class EnchantmentMending : Enchantment
    {
        public override int ID
        {
            get
            {
                return Enchantment.MENDING;
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
                return "enchantment.mending";
            }
        }
    }
}
