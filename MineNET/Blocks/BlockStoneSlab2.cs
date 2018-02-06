using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockStoneSlab2 : BlockSlabBase
    {
        public BlockStoneSlab2() : base(BlockFactory.STONE_SLAB2)
        {

        }

        public override string Name
        {
            get
            {
                return "StoneSlab2";
            }
        }
    }
}
