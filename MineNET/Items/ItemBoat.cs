namespace MineNET.Items
{
    public class ItemBoat : Item
    {
        public ItemBoat() : base(ItemFactory.BOAT)
        {

        }

        public override string Name
        {
            get
            {
                return "Boat";
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
