using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemCookedFish : ItemFood
    {
        public ItemCookedFish() : base(ItemFactory.COOKED_FISH)
        {

        }

        public override string Name
        {
            get
            {
                return "CookedFish";
            }
        }
    }
}
