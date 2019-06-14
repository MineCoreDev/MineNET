namespace MineNET.Items
{
    public class ItemStoneHoe : ItemHoe
    {
        public override int ID => ItemIDs.STONE_HOE;

        public override string Name => "Stone Hoe";

        public override ItemToolTier ToolTier => ItemToolTier.STONE;

        public override int MaxDurability => 132;
    }
}
