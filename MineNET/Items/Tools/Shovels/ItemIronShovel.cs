namespace MineNET.Items
{
    public class ItemIronShovel : ItemShovel
    {
        public override int ID => ItemIDs.IRON_SHOVEL;

        public override string Name => "Iron Shovel";

        public override ItemToolTier ToolTier => ItemToolTier.IRON;

        public override int MaxDurability => 251;
    }
}
