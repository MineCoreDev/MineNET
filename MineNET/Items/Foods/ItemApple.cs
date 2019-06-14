namespace MineNET.Items
{
    public class ItemApple : ItemFood
    {
        public override int ID => ItemIDs.APPLE;

        public override string Name => "Apple";

        public override int FoodRestore => 4;

        public override float SaturationRestore => 2.4f;
    }
}
