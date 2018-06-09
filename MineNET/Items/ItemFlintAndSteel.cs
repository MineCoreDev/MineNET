namespace MineNET.Items
{
    public class ItemFlintAndSteel : ItemTool
    {
        public ItemFlintAndSteel() : base(ItemFactory.FLINT_AND_STEEL)
        {

        }

        public override string Name
        {
            get
            {
                return "FlintAndSteel";
            }
        }

        public override bool IsFlintAndSteel
        {
            get
            {
                return true;
            }
        }

        public override int MaxDurability
        {
            get
            {
                return 65;
            }
        }
    }
}
