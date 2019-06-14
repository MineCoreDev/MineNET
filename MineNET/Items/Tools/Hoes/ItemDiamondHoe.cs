namespace MineNET.Items
{
    public class ItemDiamondHoe : ItemHoe
    {
        public override int ID => ItemIDs.DIAMOND_HOE;

        public override string Name => "Diamond Hoe";

        public override ItemToolTier ToolTier => ItemToolTier.DIAMOND;

        public override int MaxDurability => 1562;
    }
}
