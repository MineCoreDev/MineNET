namespace MineNET.Items
{
    public class ItemDiamondAxe : ItemAxe
    {
        public override int ID => ItemIDs.DIAMOND_AXE;

        public override string Name => "Diamond Axe";

        public override ItemToolTier ToolTier => ItemToolTier.DIAMOND;

        public override int MaxDurability => 1562;
    }
}
