namespace MineNET.Items
{
    public class ItemCookedPorkchop : ItemFood
    {
        public override int ID { get; } = ItemIDs.COOKED_PORKCHOP;

        public override string GetName(int damage)
        {
            return "Cooked Porkchop";
        }

        public override int FoodRestore => 8;

        public override float SaturationRestore => 12.8f;
    }
}
