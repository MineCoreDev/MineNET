namespace MineNET.Items
{
    public class ItemIronAxe : ItemAxe
    {
        public override int ID => ItemIDs.IRON_AXE;

        public override string Name => "Iron Axe";

        public override ItemToolTier ToolTier => ItemToolTier.IRON;

        public override int MaxDurability => 251;
    }
}
