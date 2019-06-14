namespace MineNET.Items
{
    public class ItemGoldenApple : ItemFood
    {
        public override int ID { get; } = ItemIDs.GOLDEN_APPLE;

        public override string GetName(int damage)
        {
            return "Golden Apple";
        }

        public override int FoodRestore => 4;

        public override float SaturationRestore => 9.6f;
    }
}
