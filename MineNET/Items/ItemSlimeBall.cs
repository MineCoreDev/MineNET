using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemSlimeBall : Item
    {
        public ItemSlimeBall() : base(ItemFactory.SLIME_BALL)
        {

        }

        public override string Name
        {
            get
            {
                return "SlimeBall";
            }
        }
    }
}
