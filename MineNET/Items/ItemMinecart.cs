namespace MineNET.Items
{
    public class ItemMinecart : Item
    {
        public override int ID { get; } = ItemIDs.MINECART;

        public override string GetName(int damage)
        {
            return "Minecart";
        }

        public override byte MaxStackSize
        {
            get
            {
                return 1;
            }
        }
    }
}
