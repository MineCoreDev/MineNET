using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemCompass : Item
    {
        public ItemCompass() : base(ItemFactory.COMPASS)
        {

        }

        public override string Name
        {
            get
            {
                return "Compass";
            }
        }
    }
}
