using MineNET.Data;
using MineNET.Entities.Players;
using MineNET.Items;

namespace MineNET.Inventories
{
    public class PlayerOffhandInventory : BaseInventory
    {
        public PlayerOffhandInventory(Player player) : base(player)
        {

        }

        public override int Size
        {
            get
            {
                return 1;
            }
        }

        public override byte Type
        {
            get
            {
                return ContainerIds.OFFHAND.GetIndex();
            }
        }

        public Item GetItem()
        {
            return base.GetItem(0);
        }

        public bool SetItem(Item item)
        {
            return base.SetItem(0, item);
        }
    }
}
