namespace MineNET.Items
{
    public class ItemSaddle : Item
    {
        public ItemSaddle() : base(ItemFactory.SADDLE)
        {

        }

        public override string Name
        {
            get
            {
                return "Saddle";
            }
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
