using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemFish : Item
    {
        public ItemFish() : base(ItemFactory.FISH)
        {

        }

        public override string Name
        {
            get
            {
                return "Fish";
            }
        }
    }
}
