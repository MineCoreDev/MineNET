namespace MineNET.Items
{
    public class ItemCarrotOnAStick : Item
    {
        public ItemCarrotOnAStick() : base(ItemFactory.CARROT_ON_A_STICK)
        {

        }

        public override string Name
        {
            get
            {
                return "CarrotOnAStick";
            }
        }

        public override byte MaxStackSize
        {
            get
            {
                return 1;
            }
        }
    }
}
