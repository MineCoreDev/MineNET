using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemCake : Item
    {
        public ItemCake() : base(ItemFactory.CAKE)
        {

        }

        public override string Name
        {
            return "Cake";
        }
    }
}
