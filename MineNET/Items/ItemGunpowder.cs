namespace MineNET.Items
{
    public class ItemGunpowder : Item
    {
        public ItemGunpowder() : base(ItemFactory.GUNPOWDER)
        {

        }

        public override string Name
        {
            get
            {
                return "Gunpowder";
            }
        }
    }
}
