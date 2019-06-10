using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemGoldenHoe : ItemHoe
    {
        public override int ID { get; } = ItemIDs.GOLDEN_HOE;

        public override string GetName(int damage)
        {
            return "Golden Hoe";
        }

        public override ItemToolTier ToolTier { get; } = ItemToolTier.GOLD;

        public override int MaxDurability { get; } = 33;
    }
}
