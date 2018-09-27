using MineNET.Entities.Players;
using MineNET.Inventories.Data;

namespace MineNET.Inventories
{
    public class PlayerEnderChestInventory : ContainerInventory
    {
        public Player Player { get; }

        public PlayerEnderChestInventory(Player player) : base(null)
        {
            this.Player = player;
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

        public override string Name
        {
            get
            {
                return "EnderChestInventory";
            }
        }

        public override void OnOpen(Player player)
        {
            base.OnOpen(player);

            //TODO:
        }

        public override void OnClose(Player player)
        {
            base.OnClose(player);

            //TODO:
        }

        public void SetHolder(InventoryHolder holder)
        {
            this.Holder = holder;
        }
    }
}
