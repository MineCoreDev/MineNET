namespace MineNET.Items
{
    public class ItemSlimeBall : Item
    {
        public override int ID { get; } = ItemIDs.SLIME_BALL;

        public override string GetName(int damage)
        {
            return "Slime Ball";
        }
    }
}
