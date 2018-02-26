using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineNET.Entities;

namespace MineNET.Inventories
{
    public abstract class ContainerInventory : BaseInventory
    {
        public ContainerInventory(InventoryHolder holder) : base(holder)
        {

        }

        public override void OnOpen(Player player)
        {
            base.OnOpen(player);
        }

        public override void OnClose(Player player)
        {
            base.OnClose(player);
        }
    }
}
