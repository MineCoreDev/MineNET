using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockSandstoneSlab : BlockSlabBase
    {
        public BlockSandstoneSlab() : base(BlockFactory.SANDSTONE_SLAB)
        {

        }

        public override string Name
        {
            get
            {
                return "SandstoneSlab";
            }
        }
    }
}
