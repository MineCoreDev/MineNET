using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemCookedChicken : ItemFood
    {
        public ItemCookedChicken() : base(ItemFactory.COOKED_CHICKEN)
        {

        }

        public override string Name
        {
            get
            {
                return "CookedChicken";
            }
        }
    }
}
