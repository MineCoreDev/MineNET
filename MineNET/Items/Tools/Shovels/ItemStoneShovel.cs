namespace MineNET.Items
{
    public class ItemStoneShovel : ItemShovel
    {
        public override int ID => ItemIDs.STONE_SHOVEL;

        public override string Name => "Stone Shovel";

        public override ItemToolTier ToolTier => ItemToolTier.STONE;

        public override int MaxDurability => 132;
    }
}
