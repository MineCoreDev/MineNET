using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockNetherBrickFence : Block
    {
        public BlockNetherBrickFence() : base(BlockFactory.NETHER_BRICK_FENCE)
        {

        }

        public override string Name
        {
            get
            {
                return "NetherBrickFence";
            }
        }
    }
}
