using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemClayBall : Item
    {
        public ItemClayBall() : base(ItemFactory.CLAY_BALL)
        {

        }

        public override string Name
        {
            get
            {
                return "ClayBall";
            }
        }
    }
}
