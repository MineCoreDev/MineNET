using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemIronDoor : ItemDoor
    {
        public ItemIronDoor() : base(ItemFactory.IRON_DOOR)
        {

        }

        public override string Name
        {
            get
            {
                return "IronDoor";
            }
        }
    }
}
