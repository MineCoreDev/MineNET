using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemReeds : Item
    {
        public ItemReeds() : base(ItemFactory.REEDS)
        {

        }

        public override string Name
        {
            get
            {
                return "Reeds";
            }
        }
    }
}
