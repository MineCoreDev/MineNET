using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemFlowerPot : Item
    {
        public ItemFlowerPot() : base(ItemFactory.FLOWER_POT)
        {

        }

        public override string Name
        {
            get
            {
                return "FlowerPot";
            }
        }
    }
}
