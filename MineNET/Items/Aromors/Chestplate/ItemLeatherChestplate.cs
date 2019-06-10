using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemLeatherChestplate : ItemChestplate
    {
        public override int ID { get; } = ItemIDs.LEATHER_CHESTPLATE;

        public override string GetName(int damage)
        {
            return "Leather Chestplate";
        }

        public override int MaxDurability { get; } = 81;
    }
}
