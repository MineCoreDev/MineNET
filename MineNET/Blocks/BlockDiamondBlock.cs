using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockDiamondBlock : BlockSolid
    {
        public BlockDiamondBlock() : base(BlockFactory.DIAMOND_BLOCK)
        {

        }

        public override string Name
        {
            get
            {
                return "DiamondBlock";
            }
        }
    }
}
