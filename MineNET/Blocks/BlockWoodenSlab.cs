using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockWoodenSlab : BlockSlabBase
    {
        public BlockWoodenSlab() : base(BlockFactory.WOODEN_SLAB)
        {

        }

        public override string Name
        {
            get
            {
                return "WoodenSlab";
            }
        }
    }
}
