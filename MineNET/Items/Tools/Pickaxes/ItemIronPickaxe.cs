namespace MineNET.Items
{
    public class ItemIronPickaxe : ItemPickaxe
    {
        public override int ID => ItemIDs.IRON_PICKAXE;

        public override string Name => "Iron Pickaxe";

        public override ItemToolTier ToolTier => ItemToolTier.IRON;

        public override int MaxDurability => 251;
    }
}
