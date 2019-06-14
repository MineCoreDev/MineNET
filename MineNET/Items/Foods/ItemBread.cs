namespace MineNET.Items
{
    public class ItemBread : ItemFood
    {
        public override int ID => ItemIDs.BREAD;

        public override string Name => "Bread";

        public override int FoodRestore => 5;

        public override float SaturationRestore => 6;
    }
}
