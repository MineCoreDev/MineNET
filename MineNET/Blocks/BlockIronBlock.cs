using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockIronBlock : BlockSolid
    {
        public BlockIronBlock() : base(BlockFactory.IRON_BLOCK)
        {

        }

        public override string Name
        {
            get
            {
                return "IronBlock";
            }
        }
    }
}
