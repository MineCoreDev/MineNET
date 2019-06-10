using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemLeatherBoots : ItemBoots
    {
        public override int ID { get; } = ItemIDs.LEATHER_BOOTS;

        public override string GetName(int damage)
        {
            return "Leather Boots";
        }

        public override int MaxDurability { get; } = 65;
    }
}
