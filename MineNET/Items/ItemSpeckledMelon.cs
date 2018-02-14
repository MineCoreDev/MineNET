using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemSpeckledMelon : Item
    {
        public ItemSpeckledMelon() : base(ItemFactory.SPECKLED_MELON)
        {

        }

        public override string Name
        {
            get
            {
                return "SpeckledMelon";
            }
        }
    }
}
