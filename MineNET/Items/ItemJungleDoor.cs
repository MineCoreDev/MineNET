using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemJungleDoor : Item
    {
        public ItemJungleDoor() : base(ItemFactory.JUNGLE_DOOR)
        {

        }

        public override string Name
        {
            get
            {
                return "JungleDoor";
            }
        }
    }
}
