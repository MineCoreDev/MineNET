namespace MineNET.Items
{
    public class ItemStonePickaxe : ItemPickaxe
    {
        public override int ID => ItemIDs.STONE_PICKAXE;

        public override string Name => "Stone Pickaxe";

        public override ItemToolTier ToolTier => ItemToolTier.STONE;

        public override int MaxDurability => 132;
    }
}
