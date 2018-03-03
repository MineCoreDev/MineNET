using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemBlazePowder : Item
    {
        public ItemBlazePowder() : base(ItemFactory.BLAZE_POWDER)
        {

        }

        public override string Name
        {
            get
            {
                return "BlazePowder";
            }
        }
    }
}
