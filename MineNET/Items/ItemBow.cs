namespace MineNET.Items
{
    public class ItemBow : ItemTool
    {
        public ItemBow() : base(ItemFactory.BOW)
        {

        }

        public override string Name
        {
            get
            {
                return "Bow";
            }
        }

        public override int MaxDurability
        {
            get
            {
                return 385;
            }
        }
    }
}
