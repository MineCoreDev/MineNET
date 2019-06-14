namespace MineNET.Items
{
    public class ItemStoneAxe : ItemAxe
    {
        public override int ID => ItemIDs.STONE_AXE;

        public override string Name => "Stone Axe";

        public override ItemToolTier ToolTier => ItemToolTier.STONE;

        public override int MaxDurability => 132;
    }
}
