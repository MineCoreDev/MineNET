namespace MineNET.Items
{
    public class ItemBoat : Item
    {
        public override int ID { get; } = ItemIDs.BOAT;

        public override string GetName(int damage)
        {
            return "Boat";
        }
    }
}
