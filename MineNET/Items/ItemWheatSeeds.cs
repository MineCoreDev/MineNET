using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemWheatSeeds : Item
    {
        public override int ID { get; } = ItemIDs.WHEAT_SEEDS;

        public override string GetName(int damage)
        {
            return "Wheat Seeds";
        }
    }
}
