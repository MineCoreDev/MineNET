namespace MineNET.Items
{
    public class ItemCookedFish : ItemFood
    {
        public override int ID => ItemIDs.COOKED_FISH;

        public override string Name => "Cooked Fish";

        public override int FoodRestore => 5;

        public override float SaturationRestore => 6;
    }
}
