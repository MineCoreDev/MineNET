using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemBakedPotato : Item
    {
        public ItemBakedPotato() : base(ItemFactory.BAKED_POTATO)
        {

        }

        public override string Name
        {
            get
            {
                return "BakedPotato";
            }
        }
    }
}
