namespace MineNET.Items
{
    public class ItemBucket : Item
    {
        public ItemBucket() : base(ItemFactory.BUCKET)
        {

        }

        public override string Name
        {
            get
            {
                return "Bucket";
            }
        }

        public override byte MaxStackSize
        {
            get
            {
                if (this.Damage == 0)
                {
                    return 16;
                }
                return 1;
            }
        }
    }
}
