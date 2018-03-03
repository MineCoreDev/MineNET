using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemEgg : Item
    {
        public ItemEgg() : base(ItemFactory.EGG)
        {

        }

        public override string Name
        {
            get
            {
                return "Egg";
            }
        }
    }
}
