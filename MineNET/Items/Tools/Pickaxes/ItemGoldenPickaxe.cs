namespace MineNET.Items
{
    public class ItemGoldenPickaxe : ItemPickaxe
    {
        public override int ID => ItemIDs.GOLDEN_PICKAXE;

        public override string Name => "Golden Pickaxe";

        public override ItemToolTier ToolTier => ItemToolTier.GOLD;

        public override int MaxDurability => 33;
    }
}
