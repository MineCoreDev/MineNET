namespace MineNET.Items
{
    public class ItemMushroomStew : ItemFood
    {
        public override int ID { get; } = ItemIDs.MUSHROOM_STEW;

        public override string GetName(int damage)
        {
            return "Mushroom Stew";
        }

        public override byte MaxStackSize => 1;

        public override int FoodRestore => 6;

        public override float SaturationRestore => 7.2f;

        public override Item Residue => Item.Get(ItemIDs.BOWL);
    }
}
