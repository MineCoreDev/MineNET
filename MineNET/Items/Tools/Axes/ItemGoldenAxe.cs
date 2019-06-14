namespace MineNET.Items
{
    public class ItemGoldenAxe : ItemAxe
    {
        public override int ID => ItemIDs.GOLDEN_AXE;

        public override string Name => "Golden Axe";

        public override ItemToolTier ToolTier => ItemToolTier.GOLD;

        public override int MaxDurability => 33;
    }
}
