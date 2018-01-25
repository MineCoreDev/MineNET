using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemRedstone : Item
    {
        public ItemRedstone() : base(ItemFactory.REDSTONE)
        {

        }

        public override string Name
        {
            get
            {
                return "Redstone";
            }
        }
    }
}
