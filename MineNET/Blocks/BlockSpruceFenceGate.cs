using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockSpruceFenceGate : BlockFenceGateBase
    {
        public BlockSpruceFenceGate() : base(BlockFactory.SPRUCE_FENCE_GATE)
        {

        }

        public override string Name
        {
            get
            {
                return "SpruceFenceGate";
            }
        }
    }
}
