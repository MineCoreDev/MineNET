using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockAcaciaFenceGate : BlockFenceGateBase
    {
        public BlockAcaciaFenceGate() : base(BlockFactory.ACACIA_FENCE_GATE)
        {

        }

        public override string Name
        {
            get
            {
                return "AcaciaFenceGate";
            }
        }
    }
}
