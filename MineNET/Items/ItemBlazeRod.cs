using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemBlazeRod : Item
    {
        public ItemBlazeRod() : base(ItemFactory.BLAZE_ROD)
        {

        }

        public override string Name
        {
            get
            {
                return "BlazeRod";
            }
        }
    }
}
