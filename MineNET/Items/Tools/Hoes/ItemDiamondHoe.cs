using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemDiamondHoe : ItemHoe
    {
        public override int ID { get; } = ItemIDs.DIAMOND_HOE;

        public override string GetName(int damage)
        {
            return "Diamond Hoe";
        }

        public override ItemToolTier ToolTier { get; } = ItemToolTier.DIAMOND;

        public override int MaxDurability { get; } = 1562;
    }
}
