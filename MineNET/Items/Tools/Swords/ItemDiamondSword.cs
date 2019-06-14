namespace MineNET.Items
{
    public class ItemDiamondSword : ItemSword
    {
        public override int ID => ItemIDs.DIAMOND_SWORD;

        public override string Name => "Diamond Sword";

        public override ItemToolTier ToolTier => ItemToolTier.DIAMOND;

        public override int MaxDurability => 1562;
    }
}
