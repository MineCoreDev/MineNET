using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemRabbitFoot : Item
    {
        public ItemRabbitFoot() : base(ItemFactory.RABBIT_FOOT)
        {

        }

        public override string Name
        {
            get
            {
                return "RabbitFoot";
            }
        }
    }
}
