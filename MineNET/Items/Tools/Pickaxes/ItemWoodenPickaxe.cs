namespace MineNET.Items
{
    public class ItemWoodenPickaxe : ItemPickaxe
    {
        public override int ID => ItemIDs.WOODEN_PICKAXE;

        public override string Name => "Wooden Pickaxe";

        public override ItemToolTier ToolTier => ItemToolTier.WOODEN;

        public override int MaxDurability => 60;
    }
}
