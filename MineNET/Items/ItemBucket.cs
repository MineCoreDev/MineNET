namespace MineNET.Items
{
    public class ItemBucket : Item
    {
        public override int ID => ItemIDs.BUCKET;

        public override string Name => "Bucket";

        public override byte MaxStackSize => 16;
    }
}
