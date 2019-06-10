using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemDiamondLeggings : ItemLeggings
    {
        public override int ID { get; } = ItemIDs.DIAMOND_LEGGINGS;

        public override string GetName(int damage)
        {
            return "Diamond Leggings";
        }

        public override int MaxDurability { get; } = 495;
    }
}
