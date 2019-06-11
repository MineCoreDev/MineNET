namespace MineNET.Items
{
    public class ItemBread : ItemFood
    {
        public override int ID { get; } = ItemIDs.BREAD;

        public override string GetName(int damage)
        {
            return "Bread";
        }

        public override int FoodRestore => 5;

        public override float SaturationRestore => 6;
    }
}
