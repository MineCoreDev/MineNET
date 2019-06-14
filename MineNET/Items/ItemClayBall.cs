namespace MineNET.Items
{
    public class ItemClayBall : Item
    {
        public override int ID { get; } = ItemIDs.CLAY_BALL;

        public override string GetName(int damage)
        {
            return "Clay Ball";
        }
    }
}
