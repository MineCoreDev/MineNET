namespace MineNET.Items
{
    public class ItemClock : Item
    {
        public override int ID { get; } = ItemIDs.CLOCK;

        public override string GetName(int damage)
        {
            return "Clock";
        }
    }
}
