using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemLingeringPotion : Item
    {
        public ItemLingeringPotion() : base(ItemFactory.LINGERING_POTION)
        {

        }

        public override string Name
        {
            get
            {
                return "LingeringPotion";
            }
        }
    }
}
