namespace MineNET.Items
{
    public class ItemIronHoe : ItemHoe
    {
        public override int ID => ItemIDs.IRON_HOE;

        public override string Name => "Iron Hoe";

        public override ItemToolTier ToolTier => ItemToolTier.IRON;

        public override int MaxDurability => 251;
    }
}
