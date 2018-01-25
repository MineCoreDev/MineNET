using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemFireworkCharge : Item
    {
        public ItemFireworkCharge() : base(ItemFactory.FIREWORK_CHARGE)
        {

        }

        public override string Name
        {
            get
            {
                return "FireworkCharge";
            }
        }
    }
}
