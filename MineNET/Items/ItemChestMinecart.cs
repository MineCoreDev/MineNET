namespace MineNET.Items
{
    public class ItemChestMinecart : Item
    {
        public override int ID { get; } = ItemIDs.CHEST_MINECART;

        public override string GetName(int damage)
        {
            return "Chest Minecart";
        }
    }
}
