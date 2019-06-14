namespace MineNET.Items
{
    public class ItemCookedPorkchop : ItemFood
    {
        public override int ID => ItemIDs.COOKED_PORKCHOP;

        public override string Name => "Cooked Porkchop";

        public override int FoodRestore => 8;

        public override float SaturationRestore => 12.8f;
    }
}
