using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemMap : Item
    {
        public ItemMap() : base(ItemFactory.MAP)
        {

        }

        public override string Name
        {
            get
            {
                return "Map";
            }
        }
    }
}
