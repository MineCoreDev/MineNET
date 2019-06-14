namespace MineNET.Items
{
    public class ItemPaper : Item
    {
        public override int ID { get; } = ItemIDs.PAPER;

        public override string GetName(int damage)
        {
            return "Paper";
        }
    }
}
