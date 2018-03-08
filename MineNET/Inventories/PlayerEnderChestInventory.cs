using MineNET.Entities.Players;
using MineNET.Inventories.Data;

namespace MineNET.Inventories
{
    public class PlayerEnderChestInventory : ContainerInventory
    {
        public PlayerEnderChestInventory(InventoryHolder holder) : base(holder)
        {

        }

        public override int Size
        {
            get
            {
                return 27;
            }
        }

        public override byte Type
        {
            get
            {
                return InventoryType.CONTAINER.GetIndex();
            }
        }

        public override void OnOpen(Player player)
        {
            base.OnOpen(player);

            //TODO
        }

        public override void OnClose(Player player)
        {
            base.OnClose(player);

            //TODO
        }
    }
}
