using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockDoubleWoodenSlab : BlockSolid
    {
        public BlockDoubleWoodenSlab() : base(BlockFactory.DOUBLE_WOODEN_SLAB)
        {

        }

        public override string Name
        {
            get
            {
                return "DoubleWoodenSlab";
            }
        }
    }
}
