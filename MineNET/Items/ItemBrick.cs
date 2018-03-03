using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemBrick : Item
    {
        public ItemBrick() : base(ItemFactory.BRICK)
        {

        }

        public override string Name
        {
            get
            {
                return "Brick";
            }
        }
    }
}
