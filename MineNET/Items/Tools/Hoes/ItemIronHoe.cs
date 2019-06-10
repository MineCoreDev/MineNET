using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemIronHoe : ItemHoe
    {
        public override int ID { get; } = ItemIDs.IRON_HOE;

        public override string GetName(int damage)
        {
            return "Iron Hoe";
        }

        public override ItemToolTier ToolTier { get; } = ItemToolTier.IRON;

        public override int MaxDurability { get; } = 251;
    }
}
