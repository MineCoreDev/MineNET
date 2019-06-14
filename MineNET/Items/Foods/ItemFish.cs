namespace MineNET.Items
{
    public class ItemFish : ItemFood
    {
        public override int ID { get; } = ItemIDs.FISH;

        public override string GetName(int damage)
        {
            return "Fish";
        }

        public override int FoodRestore => 2;

        public override float SaturationRestore => 0.4f;
    }
}
