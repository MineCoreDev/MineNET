namespace MineNET.Items
{
    public class ItemGoldenShovel : ItemShovel
    {
        public override int ID => ItemIDs.GOLDEN_SHOVEL;

        public override string Name => "Golden Shovel";

        public override ItemToolTier ToolTier => ItemToolTier.GOLD;

        public override int MaxDurability => 33;
    }
}
