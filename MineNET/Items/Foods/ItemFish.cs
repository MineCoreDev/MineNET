namespace MineNET.Items
{
    public class ItemFish : ItemFood
    {
        public override int ID => ItemIDs.FISH;

        public override string Name => "Fish";

        public override int FoodRestore => 2;

        public override float SaturationRestore => 0.4f;
    }
}
