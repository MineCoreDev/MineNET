using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockFenceGate : BlockFenceGateBase
    {
        public BlockFenceGate() : base(BlockFactory.FENCE_GATE)
        {

        }

        public override string Name
        {
            get
            {
                return "FenceGate";
            }
        }
    }
}
