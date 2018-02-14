using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemCookedMutton : ItemFood
    {
        public ItemCookedMutton() : base(ItemFactory.COOKED_MUTTON)
        {

        }

        public override string Name
        {
            get
            {
                return "CookedMutton";
            }
        }
    }
}
