using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemDiamondChestplate : ItemChestplate
    {
        public override int ID { get; } = ItemIDs.DIAMOND_CHESTPLATE;

        public override string GetName(int damage)
        {
            return "Diamond Chestplate";
        }

        public override int MaxDurability { get; } = 528;
    }
}
