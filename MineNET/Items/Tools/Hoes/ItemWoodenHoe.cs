namespace MineNET.Items
{
    public class ItemWoodenHoe : ItemHoe
    {
        public override int ID => ItemIDs.WOODEN_HOE;

        public override string Name => "Wooden Hoe";

        public override ItemToolTier ToolTier => ItemToolTier.WOODEN;

        public override int MaxDurability => 60;
    }
}
