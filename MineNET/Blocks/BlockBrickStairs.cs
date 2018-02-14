using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockBrickStairs : BlockStairsBase
    {
        public BlockBrickStairs() : base(BlockFactory.BRICK_STAIRS)
        {

        }

        public override string Name
        {
            get
            {
                return "BrickStairs";
            }
        }
    }
}
