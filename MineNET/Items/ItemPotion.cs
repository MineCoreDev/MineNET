using MineNET.Entities.Players;
using MineNET.Items.Data;

namespace MineNET.Items
{
    public class ItemPotion : Item, Consumeable
    {
        public ItemPotion() : base(ItemFactory.POTION)
        {

        }

        public override string Name
        {
            get
            {
                return "Potion";
            }
        }

        public override byte MaxStackSize
        {
            get
            {
                return 1;
            }
        }

        public void OnConsume(Player player)
        {
            //TODO
        }
    }
}
