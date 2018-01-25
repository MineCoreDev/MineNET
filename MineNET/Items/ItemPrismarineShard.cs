using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Items
{
    public class ItemPrismarineShard : Item
    {
        public ItemPrismarineShard() : base(ItemFactory.PRISMARINE_SHARD)
        {

        }

        public override string Name
        {
            get
            {
                return "PrismarineShard";
            }
        }
    }
}
