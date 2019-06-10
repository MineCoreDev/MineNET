using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemIronLeggings : ItemLeggings
    {
        public override int ID { get; } = ItemIDs.IRON_LEGGINGS;

        public override string GetName(int damage)
        {
            return "Iron Leggings";
        }

        public override int MaxDurability { get; } = 225;
    }
}
