using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockFlowingLava : Block
    {
        public BlockFlowingLava() : base(BlockFactory.FLOWING_LAVA)
        {

        }

        public override string Name
        {
            get
            {
                return "FlowingLava";
            }
        }
    }
}
