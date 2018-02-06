using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockUnpoweredRepeater : Block
    {
        public BlockUnpoweredRepeater() : base(BlockFactory.UNPOWERED_REPEATER)
        {

        }

        public override string Name
        {
            get
            {
                return "UnpoweredRepeater";
            }
        }
    }
}
