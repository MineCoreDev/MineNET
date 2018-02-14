using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockEmeraldBlock : BlockSolid
    {
        public BlockEmeraldBlock() : base(BlockFactory.EMERALD_BLOCK)
        {

        }

        public override string Name
        {
            get
            {
                return "EmeraldBlock";
            }
        }
    }
}
