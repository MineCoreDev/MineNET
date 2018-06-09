namespace MineNET.Items
{
    public class ItemFeather : Item
    {
        public ItemFeather() : base(ItemFactory.FEATHER)
        {

        }

        public override string Name
        {
            get
            {
                return "Feather";
            }
        }
    }
}
