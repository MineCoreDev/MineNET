using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemDragonBreath : Item
    {
        public ItemDragonBreath() : base(ItemFactory.DRAGON_BREATH)
        {

        }

        public override string Name
        {
            get
            {
                return "DragonBreath";
            }
        }
    }
}
