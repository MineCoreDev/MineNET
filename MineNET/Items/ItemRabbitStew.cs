using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemRabbitStew : Item
    {
        public ItemRabbitStew() : base(ItemFactory.RABBIT_STEW)
        {

        }

        public override string Name
        {
            get
            {
                return "RabbitStew";
            }
        }
    }
}
