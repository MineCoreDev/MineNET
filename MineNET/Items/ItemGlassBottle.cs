using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemGlassBottle : Item
    {
        public ItemGlassBottle() : base(ItemFactory.GLASS_BOTTLE)
        {

        }

        public override string Name
        {
            get
            {
                return "GlassBottle";
            }
        }
    }
}
