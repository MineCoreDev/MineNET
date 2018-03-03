using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemSnowball : Item
    {
        public ItemSnowball() : base(ItemFactory.SNOWBALL)
        {

        }

        public override string Name
        {
            get
            {
                return "Snowball";
            }
        }
    }
}
