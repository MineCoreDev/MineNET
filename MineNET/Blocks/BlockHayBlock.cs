using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockHayBlock : BlockSolid
    {
        public BlockHayBlock() : base(BlockFactory.HAY_BLOCK)
        {

        }

        public override string Name
        {
            get
            {
                return "HayBlock";
            }
        }
    }
}
