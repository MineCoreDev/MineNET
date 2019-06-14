namespace MineNET.Items
{
    public class ItemFlint : Item
    {
        public override int ID { get; } = ItemIDs.FLINT;

        public override string GetName(int damage)
        {
            return "Flint";
        }
    }
}
