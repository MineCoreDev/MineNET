namespace MineNET.Items
{
    public class ItemWoodenAxe : ItemAxe
    {
        public override int ID => ItemIDs.WOODEN_AXE;

        public override string Name => "Wooden Axe";

        public override ItemToolTier ToolTier => ItemToolTier.WOODEN;

        public override int MaxDurability => 60;
    }
}
