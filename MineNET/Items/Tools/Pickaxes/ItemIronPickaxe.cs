using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemIronPickaxe : ItemPickaxe
    {
        public override int ID { get; } = ItemIDs.IRON_PICKAXE;

        public override string GetName(int damage)
        {
            return "Iron Pickaxe";
        }
    }
}
