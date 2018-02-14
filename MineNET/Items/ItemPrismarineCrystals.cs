using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemPrismarineCrystals : Item
    {
        public ItemPrismarineCrystals() : base(ItemFactory.PRISMARINE_CRYSTALS)
        {

        }

        public override string Name
        {
            get
            {
                return "PrismarineCrystals";
            }
        }
    }
}
