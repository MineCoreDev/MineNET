namespace MineNET.Items
{
    public class ItemCookedFish : ItemFood
    {
        public override int ID { get; } = ItemIDs.COOKED_FISH;

        public override string GetName(int damage)
        {
            return "Cooked Fish";
        }

        public override int FoodRestore => 5;

        public override float SaturationRestore => 6;
    }
}
