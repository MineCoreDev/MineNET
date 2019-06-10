using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemLeatherLeggings : ItemLeggings
    {
        public override int ID { get; } = ItemIDs.LEATHER_LEGGINGS;

        public override string GetName(int damage)
        {
            return "Leather Leggings";
        }

        public override int MaxDurability { get; } = 75;
    }
}
