using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemDiamondBoots : ItemBoots
    {
        public override int ID { get; } = ItemIDs.DIAMOND_BOOTS;

        public override string GetName(int damage)
        {
            return "Diamond Boots";
        }

        public override int MaxDurability { get; } = 429;
    }
}
