using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemRabbit : Item
    {
        public ItemRabbit() : base(ItemFactory.RABBIT)
        {

        }

        public override string Name
        {
            get
            {
                return "Rabbit";
            }
        }
    }
}
