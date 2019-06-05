using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemIronShovel : ItemShovel
    {
        public override int ID { get; } = ItemIDs.IRON_SHOVEL;

        public override string GetName(int damage)
        {
            return "Iron Shovel";
        }
    }
}
