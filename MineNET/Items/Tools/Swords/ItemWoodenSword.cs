namespace MineNET.Items
{
    public class ItemWoodenSword : ItemSword
    {
        public override int ID => ItemIDs.WOODEN_SWORD;

        public override string Name => "Wooden Sword";

        public override ItemToolTier ToolTier { get; } = ItemToolTier.WOODEN;

        public override int MaxDurability { get; } = 60;
    }
}
