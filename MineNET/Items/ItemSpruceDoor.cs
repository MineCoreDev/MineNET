using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemSpruceDoor : Item
    {
        public ItemSpruceDoor() : base(ItemFactory.SPRUCE_DOOR)
        {

        }

        public override string Name
        {
            get
            {
                return "SpruceDoor";
            }
        }
    }
}
