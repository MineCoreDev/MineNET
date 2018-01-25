using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemDarkOakDoor : Item
    {
        public ItemDarkOakDoor() : base(ItemFactory.DARK_OAK_DOOR)
        {

        }

        public override string Name
        {
            get
            {
                return "DarkOakDoor";
            }
        }
    }
}
