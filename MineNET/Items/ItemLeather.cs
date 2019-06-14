namespace MineNET.Items
{
    public class ItemLeather : Item
    {
        public override int ID { get; } = ItemIDs.LEATHER;

        public override string GetName(int damage)
        {
            return "Leather";
        }
    }
}
