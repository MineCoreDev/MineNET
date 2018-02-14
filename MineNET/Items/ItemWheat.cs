using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemWheat : Item
    {
        public ItemWheat() : base(ItemFactory.WHEAT)
        {

        }

        public override string Name
        {
            get
            {
                return "Wheat";
            }
        }
    }
}
