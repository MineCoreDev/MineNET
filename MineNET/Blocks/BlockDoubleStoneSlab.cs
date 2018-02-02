using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockDoubleStoneSlab : BlockSolid
    {
        public BlockDoubleStoneSlab() : base(BlockFactory.DOUBLE_STONE_SLAB)
        {

        }

        public override string Name
        {
            get
            {
                return "DoubleStoneSlab";
            }
        }
    }
}
