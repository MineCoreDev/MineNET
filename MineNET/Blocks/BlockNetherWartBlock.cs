using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockNetherWartBlock : BlockSolid
    {
        public BlockNetherWartBlock() : base(BlockFactory.NETHER_WART_BLOCK)
        {

        }

        public override string Name
        {
            get
            {
                return "NetherWartBlock";
            }
        }
    }
}
