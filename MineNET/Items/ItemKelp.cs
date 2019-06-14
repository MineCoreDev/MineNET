namespace MineNET.Items
{
    public class ItemKelp : Item
    {
        public override int ID { get; } = ItemIDs.KELP;

        public override string GetName(int damage)
        {
            return "Kelp";
        }
    }
}
