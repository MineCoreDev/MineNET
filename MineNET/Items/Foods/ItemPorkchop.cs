namespace MineNET.Items
{
    public class ItemPorkchop : ItemFood
    {
        public override int ID { get; } = ItemIDs.PORKCHOP;

        public override string GetName(int damage)
        {
            return "Porkchop";
        }

        public override int FoodRestore => 3;

        public override float SaturationRestore => 1.8f;
    }
}
