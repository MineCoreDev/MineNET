using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockNetherBrickStairs : BlockStairsBase
    {
        public BlockNetherBrickStairs() : base(BlockFactory.NETHER_BRICK_STAIRS)
        {

        }

        public override string Name
        {
            get
            {
                return "NetherBrickStairs";
            }
        }
    }
}
