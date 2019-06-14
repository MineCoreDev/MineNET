namespace MineNET.Items
{
    public class ItemDye : Item
    {
        public override int ID { get; } = ItemIDs.DYE;

        public override string GetName(int damage)
        {
            return "Dye"; //TODO
        }
    }
}
