using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemComparator : Item
    {
        public ItemComparator() : base(ItemFactory.COMPARATOR)
        {

        }

        public override string Name
        {
            get
            {
                return "Comparator";
            }
        }
    }
}
