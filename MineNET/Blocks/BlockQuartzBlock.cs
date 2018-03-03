using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockQuartzBlock : BlockSolid
    {
        public BlockQuartzBlock() : base(BlockFactory.QUARTZ_BLOCK)
        {

        }

        public override string Name
        {
            get
            {
                return "QuartzBlock";
            }
        }
    }
}
