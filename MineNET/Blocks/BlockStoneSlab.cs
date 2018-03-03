using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockStoneSlab : BlockSlabBase
    {
        public BlockStoneSlab() : base(BlockFactory.STONE_SLAB)
        {

        }

        public override string Name
        {
            get
            {
                return "StoneSlab";
            }
        }
    }
}
