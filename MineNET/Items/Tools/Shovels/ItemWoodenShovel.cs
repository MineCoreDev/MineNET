namespace MineNET.Items
{
    public class ItemWoodenShovel : ItemShovel
    {
        public override int ID => ItemIDs.WOODEN_SHOVEL;

        public override string Name => "Wooden Shovel";

        public override ItemToolTier ToolTier => ItemToolTier.WOODEN;

        public override int MaxDurability => 60;
    }
}
