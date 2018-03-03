using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemWheatSeeds : Item
    {
        public ItemWheatSeeds() : base(ItemFactory.WHEAT_SEEDS)
        {

        }

        public override string Name
        {
            get
            {
                return "WheatSeeds";
            }
        }
    }
}
