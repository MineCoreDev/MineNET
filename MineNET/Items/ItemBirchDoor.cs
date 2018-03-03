using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemBirchDoor : Item
    {
        public ItemBirchDoor() : base(ItemFactory.BIRCH_DOOR)
        {

        }

        public override string Name
        {
            get
            {
                return "BirchDoor";
            }
        }
    }
}
