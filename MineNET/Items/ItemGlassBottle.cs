namespace MineNET.Items
{
    public class ItemGlassBottle : Item
    {
        public ItemGlassBottle() : base(ItemFactory.GLASS_BOTTLE)
        {

        }

        public override string Name
        {
            get
            {
                return "GlassBottle";
            }
        }
    }
}
