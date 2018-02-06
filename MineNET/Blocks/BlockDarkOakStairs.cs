using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockDarkOakStairs : BlockStairsBase
    {
        public BlockDarkOakStairs() : base(BlockFactory.DARK_OAK_STAIRS)
        {

        }

        public override string Name
        {
            get
            {
                return "DarkOakStairs";
            }
        }
    }
}
