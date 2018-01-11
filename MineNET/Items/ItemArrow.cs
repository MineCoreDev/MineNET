using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemArrow : Item
    {
        public ItemArrow() : base(ItemFactory.ARROW)
        {

        }

        public override string Name
        {
            get
            {
                return "Arrow";
            }
        }
    }
}
