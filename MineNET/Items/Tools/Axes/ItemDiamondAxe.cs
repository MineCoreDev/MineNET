using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemDiamondAxe : ItemAxe
    {
        public override int ID { get; } = ItemIDs.DIAMOND_AXE;

        public override string GetName(int damage)
        {
            return "Diamond Axe";
        }

        public override ItemToolTier ToolTier { get; } = ItemToolTier.DIAMOND;

        public override int MaxDurability { get; } = 1562;
    }
}
