using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemIronChestplate : ItemChestplate
    {
        public override int ID { get; } = ItemIDs.IRON_CHESTPLATE;

        public override string GetName(int damage)
        {
            return "Iron Chestplate";
        }

        public override int MaxDurability { get; } = 241;
    }
}
