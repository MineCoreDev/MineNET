using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockMovingBlock : BlockTransparent
    {
        public BlockMovingBlock() : base(BlockFactory.MOVING_BLOCK)
        {

        }

        public override string Name
        {
            get
            {
                return "MovingBlock";
            }
        }
    }
}
