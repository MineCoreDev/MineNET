using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockPoweredRepeater : Block
    {
        public BlockPoweredRepeater() : base(BlockFactory.POWERED_REPEATER)
        {

        }

        public override string Name
        {
            get
            {
                return "PoweredRepeater";
            }
        }
    }
}
