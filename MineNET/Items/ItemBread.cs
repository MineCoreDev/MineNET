using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemBread : ItemFood
    {
        public ItemBread() : base(ItemFactory.BREAD)
        {

        }

        public override string Name
        {
            get
            {
                return "Bread";
            }
        }
    }
}
