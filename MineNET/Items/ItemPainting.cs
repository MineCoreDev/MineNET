namespace MineNET.Items
{
    public class ItemPainting : Item
    {
        public override int ID { get; } = ItemIDs.PAINTING;

        public override string GetName(int damage)
        {
            return "Painting";
        }
    }
}
