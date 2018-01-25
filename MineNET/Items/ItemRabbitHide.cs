using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemRabbitHide : Item
    {
        public ItemRabbitHide() : base(ItemFactory.RABBIT_HIDE)
        {

        }

        public override string Name
        {
            get
            {
                return "RabbitHide";
            }
        }
    }
}
