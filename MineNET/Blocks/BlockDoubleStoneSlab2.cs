using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockDoubleStoneSlab2 : BlockSolid
    {
        public BlockDoubleStoneSlab2() : base(BlockFactory.DOUBLE_STONE_SLAB2)
        {

        }

        public override string Name
        {
            get
            {
                return "DoubleStoneSlab2";
            }
        }
    }
}
