namespace MineNET.Items
{
    public class ItemGoldenSword : ItemSword
    {
        public override int ID => ItemIDs.GOLDEN_SWORD;

        public override string Name => "Golden Sword";

        public override ItemToolTier ToolTier => ItemToolTier.GOLD;

        public override int MaxDurability => 33;
    }
}
