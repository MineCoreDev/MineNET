using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemGoldenBoots : ItemBoots
    {
        public override int ID { get; } = ItemIDs.GOLDEN_BOOTS;

        public override string GetName(int damage)
        {
            return "Golden Boots";
        }

        public override int MaxDurability { get; } = 91;
    }
}
