using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemIronAxe : ItemAxe
    {
        public override int ID { get; } = ItemIDs.IRON_AXE;

        public override string GetName(int damage)
        {
            return "Iron Axe";
        }
    }
}
