using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemWoodenAxe : ItemAxe
    {
        public override int ID { get; } = ItemIDs.WOODEN_AXE;

        public override string GetName(int damage)
        {
            return "Wooden Axe";
        }

        public override ItemToolTier ToolTier { get; } = ItemToolTier.WOODEN;

        public override int MaxDurability { get; } = 60;
    }
}
