using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemMelonSeeds : Item
    {
        public ItemMelonSeeds() : base(ItemFactory.MELON_SEEDS)
        {

        }

        public override string Name
        {
            get
            {
                return "MelonSeeds";
            }
        }
    }
}
