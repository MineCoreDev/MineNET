using System.Collections.Generic;
using MineNET.Entities.Players;
using MineNET.Items;
using MineNET.Network.MinecraftPackets;
using MineNET.Values;

namespace MineNET.Inventories
{
    public abstract class ContainerInventory : BaseInventory
    {
        public ContainerInventory(InventoryHolder holder, Dictionary<int, ItemStack> items = null) : base(holder, items)
        {

        }

        public override void OnOpen(Player player)
        {
            base.OnOpen(player);

            ContainerOpenPacket pk = new ContainerOpenPacket();
            pk.WindowId = this.Type;
            pk.Type = this.Type;
            InventoryHolder holder = this.Holder;
            pk.Position = new BlockCoordinate3D((int) holder.X, (int) holder.Y, (int) holder.Z);
            player.SendPacket(pk);
        }

        public override void OnClose(Player player)
        {
            ContainerClosePacket pk = new ContainerClosePacket();
            pk.WindowId = this.Type;
            player.SendPacket(pk);

            base.OnClose(player);
        }
    }
}
