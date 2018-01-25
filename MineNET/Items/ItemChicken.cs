using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemChicken : ItemFood
    {
        public ItemChicken() : base(ItemFactory.CHICKEN)
        {

        }

        public override string Name
        {
            get
            {
                return "Chicken";
            }
        }
    }
}
