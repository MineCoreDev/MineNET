using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemPumpkinPie : Item
    {
        public ItemPumpkinPie() : base(ItemFactory.PUMPKIN_PIE)
        {

        }

        public override string Name
        {
            get
            {
                return "PumpkinPie";
            }
        }
    }
}
