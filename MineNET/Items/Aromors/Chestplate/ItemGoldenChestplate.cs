using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemGoldenChestplate : ItemChestplate
    {
        public override int ID { get; } = ItemIDs.GOLDEN_CHESTPLATE;

        public override string GetName(int damage)
        {
            return "Golden Chestplate";
        }

        public override int MaxDurability { get; } = 113;
    }
}
