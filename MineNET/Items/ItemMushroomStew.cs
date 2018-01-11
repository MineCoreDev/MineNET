using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemMushroomStew : Item
    {
        public ItemMushroomStew() : base(ItemFactory.MUSHROOM_STEW)
        {

        }

        public override string Name
        {
            get
            {
                return "MushroomStew";
            }
        }
    }
}
