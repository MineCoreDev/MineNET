namespace MineNET.Items
{
    public class ItemDoorWooden : ItemDoor
    {
        public ItemDoorWooden() : base(ItemFactory.WOODEN_DOOR)
        {

        }

        public override string Name
        {
            get
            {
                return "WoodenDoor";
            }
        }
    }
}
