namespace MineNET.Items
{
    public class ItemPorkchop : ItemFood
    {
        public override int ID => ItemIDs.PORKCHOP;

        public override string Name => "Porkshop";

        public override int FoodRestore => 3;

        public override float SaturationRestore => 1.8f;
    }
}
