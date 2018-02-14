using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemBowl : Item
    {
        public ItemBowl() : base(ItemFactory.BOWL)
        {

        }

        public override string Name
        {
            get
            {
                return "Bowl";
            }
        }
    }
}
