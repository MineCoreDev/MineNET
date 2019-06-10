using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemGoldenPickaxe : ItemPickaxe
    {
        public override int ID { get; } = ItemIDs.GOLDEN_PICKAXE;

        public override string GetName(int damage)
        {
            return "Golden Pickaxe";
        }

        public override ItemToolTier ToolTier { get; } = ItemToolTier.GOLD;

        public override int MaxDurability { get; } = 33;
    }
}
