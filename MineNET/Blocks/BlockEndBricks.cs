using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockEndBricks : BlockSolid
    {
        public BlockEndBricks() : base(BlockFactory.END_BRICKS)
        {

        }

        public override string Name
        {
            get
            {
                return "EndBricks";
            }
        }
    }
}
