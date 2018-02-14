using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemPumpkinSeeds : Item
    {
        public ItemPumpkinSeeds() : base(ItemFactory.PUMPKIN_SEEDS)
        {

        }

        public override string Name
        {
            get
            {
                return "PumpkinSeeds";
            }
        }
    }
}
