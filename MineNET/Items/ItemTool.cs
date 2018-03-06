namespace MineNET.Items
{
    public abstract class ItemTool : Item
    {
        public ItemTool(int id) : base(id)
        {

        }

        public override bool IsTool
        {
            get
            {
                return true;
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
