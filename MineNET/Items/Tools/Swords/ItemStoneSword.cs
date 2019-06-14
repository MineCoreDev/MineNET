namespace MineNET.Items
{
    public class ItemStoneSword : ItemSword
    {
        public override int ID => ItemIDs.STONE_SWORD;

        public override string Name => "Stone Sword";

        public override ItemToolTier ToolTier => ItemToolTier.STONE;

        public override int MaxDurability => 132;
    }
}
