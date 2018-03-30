namespace MineNET.Items
{
    public class ItemShears : ItemTool
    {
        public ItemShears() : base(ItemFactory.SHEARS)
        {

        }

        public override string Name
        {
            get
            {
                return "Shears";
            }
        }

        public override bool IsShears
        {
            get
            {
                return true;
            }
        }
    }
}
