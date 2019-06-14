namespace MineNET.Items
{
    public class ItemGlowstoneDust : Item
    {
        public override int ID { get; } = ItemIDs.GLOWSTONE_DUST;

        public override string GetName(int damage)
        {
            return "Glowstone Dust";
        }
    }
}
