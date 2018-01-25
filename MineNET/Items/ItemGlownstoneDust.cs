using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemGlownstoneDust : Item
    {
        public ItemGlownstoneDust() : base(ItemFactory.GLOWSTONE_DUST)
        {

        }

        public override string Name
        {
            get
            {
                return "GlowstoneDust";
            }
        }
    }
}
