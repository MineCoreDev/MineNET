using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemQuartz : Item
    {
        public ItemQuartz() : base(ItemFactory.QUARTZ)
        {

        }

        public override string Name
        {
            get
            {
                return "Quartz";
            }
        }
    }
}
