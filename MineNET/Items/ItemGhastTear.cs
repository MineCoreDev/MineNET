using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemGhastTear : Item
    {
        public ItemGhastTear() : base(ItemFactory.GHAST_TEAR)
        {

        }

        public override string Name
        {
            get
            {
                return "GhastTear";
            }
        }
    }
}
