using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    class ItemAppleenchanted : ItemFood
    {
        public ItemAppleenchanted() : base(ItemFactory.APPLEENCHANTED)
        {

        }

        public override string Name
        {
            get
            {
                return "Appleenchanted";
            }
        }
    }
}
