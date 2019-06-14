namespace MineNET.Items
{
    public class ItemSnowball : Item
    {
        public override int ID { get; } = ItemIDs.SNOWBALL;

        public override string GetName(int damage)
        {
            return "Snowball";
        }
    }
}
