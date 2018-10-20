namespace MineNET.Items.Enchantments
{
    public class EnchantmentFeatherFalling : Enchantment
    {
        public override int ID
        {
            get
            {
                return Enchantment.FEATHER_FALLING;
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
                return "enchantment.feather_falling";
            }
        }
    }
}
