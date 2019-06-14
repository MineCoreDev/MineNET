namespace MineNET.Items
{
    public class ItemBucket : Item
    {
        public override int ID { get; } = ItemIDs.BUCKET;

        public override string GetName(int damage)
        {
            return "Bucket";
        }

        public override byte MaxStackSize
        {
            get
            {
                return 16;
            }
        }
    }
}
