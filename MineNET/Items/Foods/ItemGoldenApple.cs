namespace MineNET.Items
{
    public class ItemGoldenApple : ItemFood
    {
        public override int ID => ItemIDs.GOLDEN_APPLE;

        public override string Name => "Golden Apple";

        public override int FoodRestore => 4;

        public override float SaturationRestore => 9.6f;
    }
}
