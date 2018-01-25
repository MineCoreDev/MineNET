using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemNetherStar : Item
    {
        public ItemNetherStar() : base(ItemFactory.NETHER_STAR)
        {

        }

        public override string Name
        {
            get
            {
                return "NetherStar";
            }
        }
    }
}
