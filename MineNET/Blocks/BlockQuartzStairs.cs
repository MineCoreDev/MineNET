using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockQuartzStairs : BlockStairsBase
    {
        public BlockQuartzStairs() : base(BlockFactory.QUARTZ_STAIRS)
        {

        }

        public override string Name
        {
            get
            {
                return "QuartzStairs";
            }
        }
    }
}
