using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemWoodenHoe : ItemHoe
    {
        public override int ID { get; } = ItemIDs.WOODEN_HOE;

        public override string GetName(int damage)
        {
            return "Wooden Hoe";
        }

        public override ItemToolTier ToolTier { get; } = ItemToolTier.WOODEN;

        public override int MaxDurability { get; } = 60;
    }
}
