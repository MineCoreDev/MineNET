using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockOakStairs : BlockStairsBase
    {
        public BlockOakStairs() : base(BlockFactory.OAK_STAIRS)
        {

        }

        public override string Name
        {
            get
            {
                return "OakStairs";
            }
        }
    }
}
