using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemNetherBrick : Item
    {
        public ItemNetherBrick() : base(ItemFactory.NETHERBRICK)
        {

        }

        public override string Name
        {
            get
            {
                return "NetherBricks";
            }
        }
    }
}
