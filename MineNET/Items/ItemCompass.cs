namespace MineNET.Items
{
    public class ItemCompass : Item
    {
        public override int ID { get; } = ItemIDs.COMPASS;

        public override string GetName(int damage)
        {
            return "Compass";
        }
    }
}
