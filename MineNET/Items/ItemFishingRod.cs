using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemFishingRod : Item
    {
        public ItemFishingRod() : base(ItemFactory.FISHING_ROD)
        {

        }

        public override string Name
        {
            get
            {
                return "FishingRod";
            }
        }
    }
}
