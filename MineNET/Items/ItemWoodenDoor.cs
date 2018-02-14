using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemWoodenDoor : ItemDoor
    {
        public ItemWoodenDoor() : base(ItemFactory.WOODEN_DOOR)
        {

        }

        public override string Name
        {
            get
            {
                return "WoodenDoor";
            }
        }
    }
}
