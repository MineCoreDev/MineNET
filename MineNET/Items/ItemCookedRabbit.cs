using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemCookedRabbit : ItemFood
    {
        public ItemCookedRabbit() : base(ItemFactory.COOKED_RABBIT)
        {

        }

        public override string Name
        {
            get
            {
                return "CookedRabbit";
            }
        }
    }
}
