using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockStoneBrickStairs : BlockStairsBase
    {
        public BlockStoneBrickStairs() : base(BlockFactory.STONE_BRICK_STAIRS)
        {

        }

        public override string Name
        {
            get
            {
                return "StoneBrickStairs";
            }
        }
    }
}
