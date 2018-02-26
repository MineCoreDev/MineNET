using MineNET.Entities;
using MineNET.Items;

namespace MineNET.Inventories
{
    public class PlayerInventory : BaseInventory
    {
        private int mainHand = 0;

        public PlayerInventory(Player player) : base(player)
        {

        }

        public override int Size
        {
            get
            {
                return 36 + 4 + 1;
            }
        }

        public override string Name
        {
            get
            {
                return "Player";
            }
        }

        public override int Type
        {
            get
            {
                return -1;
            }
        }

        public Item GetItemMainHand()
        {
            return this.GetItem(this.mainHand);
        }
    }
}
