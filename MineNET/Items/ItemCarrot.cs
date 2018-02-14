using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemCarrot : Item
    {
        public ItemCarrot() : base(ItemFactory.CARROT)
        {

        }

        public override string Name
        {
            get
            {
                return "Carrot";
            }
        }
    }
}
