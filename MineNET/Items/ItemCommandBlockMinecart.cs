using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemCommandBlockMinecart : Item
    {
        public ItemCommandBlockMinecart() : base(ItemFactory.COMMAND_BLOCK_MINECART)
        {

        }

        public override string Name
        {
            get
            {
                return "CommandBlockMinecart";
            }
        }
    }
}
