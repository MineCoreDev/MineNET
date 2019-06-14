namespace MineNET.Items
{
    public class ItemReeds : Item
    {
        public override int ID { get; } = ItemIDs.REEDS;

        public override string GetName(int damage)
        {
            return "Reeds";
        }
    }
}
