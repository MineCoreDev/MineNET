namespace MineNET.Items
{
    public class ItemDiamondPickaxe : ItemPickaxe
    {
        public override int ID => ItemIDs.DIAMOND_PICKAXE;

        public override string Name => "Diamond Pickaxe";

        public override ItemToolTier ToolTier => ItemToolTier.DIAMOND;

        public override int MaxDurability => 1562;
    }
}
