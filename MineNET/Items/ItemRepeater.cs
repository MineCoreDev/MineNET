using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemRepeater : Item
    {
        public ItemRepeater() : base(ItemFactory.REPEATER)
        {

        }

        public override string Name
        {
            get
            {
                return "Repeater";
            }
        }
    }
}
