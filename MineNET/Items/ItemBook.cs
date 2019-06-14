namespace MineNET.Items
{
    public class ItemBook : Item
    {
        public override int ID { get; } = ItemIDs.BOOK;

        public override string GetName(int damage)
        {
            return "Book";
        }
    }
}
