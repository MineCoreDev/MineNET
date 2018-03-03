using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockSpruceStairs : BlockStairsBase
    {
        public BlockSpruceStairs() : base(BlockFactory.SPRUCE_STAIRS)
        {

        }

        public override string Name
        {
            get
            {
                return "SpruceStairs";
            }
        }
    }
}
