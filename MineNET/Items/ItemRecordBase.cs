namespace MineNET.Items
{
    public abstract class ItemRecordBase : Item
    {
        public ItemRecordBase(int id) : base(id)
        {

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
