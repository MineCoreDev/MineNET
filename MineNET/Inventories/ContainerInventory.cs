using System.Collections.Generic;
using MineNET.Entities;
using MineNET.Items;
using MineNET.Network.Packets;
using MineNET.Values;

namespace MineNET.Inventories
{
    public abstract class ContainerInventory : BaseInventory
    {
        public ContainerInventory(InventoryHolder holder, Dictionary<int, Item> items = null) : base(holder, items)
        {

        }

        public override void OnOpen(Player player)
        {
            base.OnOpen(player);

            ContainerOpenPacket pk = new ContainerOpenPacket();
            pk.WindowId = this.Type;
            pk.Type = this.Type;
            InventoryHolder holder = this.Holder;
            pk.Vector3 = new Vector3(holder.X, holder.Y, holder.Z);
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
