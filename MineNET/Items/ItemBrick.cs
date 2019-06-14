namespace MineNET.Items
{
    public class ItemBrick : Item
    {
        public override int ID { get; } = ItemIDs.BRICK;

        public override string GetName(int damage)
        {
            return "Brick";
        }
    }
}
