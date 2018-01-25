using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemBrichDoor : Item
    {
        public ItemBrichDoor() : base(ItemFactory.BRICK)
        {

        }

        public override string Name
        {
            get
            {
                return "BrichDoor";
            }
        }
    }
}
