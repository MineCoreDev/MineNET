namespace MineNET.Items
{
    public class ItemEgg : Item
    {
        public override int ID { get; } = ItemIDs.EGG;

        public override string GetName(int damage)
        {
            return "Egg";
        }
    }
}
