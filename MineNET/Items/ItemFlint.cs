using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemFlint : Item
    {
        public ItemFlint() : base(ItemFactory.FLINT)
        {

        }

        public override string Name
        {
            get
            {
                return "Flint";
            }
        }
    }
}
