namespace MineNET.Items
{
    public class ItemEgg : Item
    {
        public ItemEgg() : base(ItemFactory.EGG)
        {

        }

        public override string Name
        {
            get
            {
                return "Egg";
            }
        }

        public override byte MaxStackSize
        {
            get
            {
                return 16;
            }
        }
    }
}
