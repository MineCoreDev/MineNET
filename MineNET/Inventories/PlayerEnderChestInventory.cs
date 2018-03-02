using MineNET.Entities;
using MineNET.Inventories.Data;

namespace MineNET.Inventories
{
    public class PlayerEnderChestInventory : ContainerInventory
    {
        private Player player;

        public PlayerEnderChestInventory(Player player) : base(null)
        {
            this.player = player;
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
                return (byte) InventoryType.CONTAINER;
            }
        }

        public void OnOpen(Player player, InventoryHolder holder)
        {
            this.Holder = holder;

            base.OnOpen(player);
        }

        public override void OnClose(Player player)
        {
            if (this.Holder != null)
            {
                this.Holder = null;
            }
            base.OnClose(player);
        }
    }
}
