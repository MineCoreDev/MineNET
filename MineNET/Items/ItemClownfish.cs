using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    class ItemClownfish : Item
    {
        public ItemClownfish() : base(ItemFactory.CLOWNFISH)
        {

        }

        public override string Name
        {
            get
            {
                return "Clownfish";
            }
        }
    }
}
