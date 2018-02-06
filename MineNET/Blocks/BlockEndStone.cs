using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockEndStone : BlockSolid
    {
        public BlockEndStone() : base(BlockFactory.END_STONE)
        {

        }

        public override string Name
        {
            get
            {
                return "EndStone";
            }
        }
    }
}
