using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockCoalBlock : BlockSolid
    {
        public BlockCoalBlock() : base(BlockFactory.COAL_BLOCK)
        {

        }

        public override string Name
        {
            get
            {
                return "CoalBlock";
            }
        }
    }
}
