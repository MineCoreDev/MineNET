using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockBirchFenceGate : BlockFenceGateBase
    {
        public BlockBirchFenceGate() : base(BlockFactory.BIRCH_FENCE_GATE)
        {

        }

        public override string Name
        {
            get
            {
                return "BirchFenceGate";
            }
        }
    }
}
