using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemStick : Item
    {
        public ItemStick() : base(ItemFactory.STICK)
        {

        }

        public override string Name
        {
            get
            {
                return "Stick";
            }
        }
    }
}
