using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockFence : Block
    {
        public BlockFence() : base(BlockFactory.FENCE)
        {

        }

        public override string Name
        {
            get
            {
                return "Fence";
            }
        }
    }
}
