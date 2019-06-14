namespace MineNET.Items
{
    public class ItemIronSword : ItemSword
    {
        public override int ID => ItemIDs.IRON_SWORD;

        public override string Name => "Iron Sword";

        public override ItemToolTier ToolTier => ItemToolTier.IRON;

        public override int MaxDurability => 251;
    }
}
