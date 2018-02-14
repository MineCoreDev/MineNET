using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemLead : Item
    {
        public ItemLead() : base(ItemFactory.LEAD)
        {

        }

        public override string Name
        {
            get
            {
                return "Lead";
            }
        }
    }
}
