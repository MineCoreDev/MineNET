namespace MineNET.Items
{
    public class ItemFilledMap : Item
    {
        public ItemFilledMap() : base(ItemFactory.FILLED_MAP)
        {

        }

        public override string Name
        {
            get
            {
                return "FilledMap";
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
