using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemShears : Item
    {
        public ItemShears() : base(ItemFactory.SHEARS)
        {

        }

        public override string Name
        {
            get
            {
                return "Shears";
            }
        }
    }
}
