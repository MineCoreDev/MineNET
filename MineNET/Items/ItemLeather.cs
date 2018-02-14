using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemLeather : Item
    {
        public ItemLeather() : base(ItemFactory.LEATHER)
        {

        }

        public override string Name
        {
            get
            {
                return "Leather";
            }
        }
    }
}
