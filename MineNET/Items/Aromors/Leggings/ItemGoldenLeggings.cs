using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemGoldenLeggings : ItemLeggings
    {
        public override int ID { get; } = ItemIDs.GOLDEN_LEGGINGS;

        public override string GetName(int damage)
        {
            return "Golden Leggings";
        }

        public override int MaxDurability { get; } = 105;
    }
}
