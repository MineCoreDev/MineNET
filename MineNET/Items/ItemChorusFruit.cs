using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemChorusFruit : Item
    {
        public ItemChorusFruit() : base(ItemFactory.CHORUS_FRUIT)
        {

        }

        public override string Name
        {
            get
            {
                return "ChorusFruit";
            }
        }
    }
}
