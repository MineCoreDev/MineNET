using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemPoppedChorusFruit : Item
    {
        public ItemPoppedChorusFruit() : base(ItemFactory.POPPED_CHORUS_FRUIT)
        {

        }

        public override string Name
        {
            get
            {
                return "PoppedChorusFruit";
            }
        }
    }
}
