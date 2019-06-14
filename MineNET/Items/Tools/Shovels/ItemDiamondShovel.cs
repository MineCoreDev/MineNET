namespace MineNET.Items
{
    public class ItemDiamondShovel : ItemShovel
    {
        public override int ID => ItemIDs.DIAMOND_SHOVEL;

        public override string Name => "Diamond Shovel";

        public override ItemToolTier ToolTier => ItemToolTier.DIAMOND;

        public override int MaxDurability => 1562;
    }
}
