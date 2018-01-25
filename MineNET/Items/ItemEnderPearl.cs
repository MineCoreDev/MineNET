using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemEnderPearl : Item
    {
        public ItemEnderPearl() : base(ItemFactory.ENDER_PEARL)
        {

        }

        public override string Name
        {
            get
            {
                return "EnderPearl";
            }
        }
    }
}
