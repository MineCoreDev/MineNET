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

        public override ItemToolTier ToolTier { get; } = ItemToolTier.IRON;

        public override int MaxDurability { get; } = 251;
    }
}
