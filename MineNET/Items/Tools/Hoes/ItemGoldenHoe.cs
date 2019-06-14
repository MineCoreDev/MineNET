namespace MineNET.Items
{
    public class ItemGoldenHoe : ItemHoe
    {
        public override int ID => ItemIDs.GOLDEN_HOE;

        public override string Name => "Golden Hoe";

        public override ItemToolTier ToolTier => ItemToolTier.GOLD;

        public override int MaxDurability => 33;
    }
}
