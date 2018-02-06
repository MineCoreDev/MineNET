using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockMossyCobblestone : BlockSolid
    {
        public BlockMossyCobblestone() : base(BlockFactory.MOSSY_COBBLESTONE)
        {

        }

        public override string Name
        {
            get
            {
                return "MossyCobblestone";
            }
        }
    }
}
