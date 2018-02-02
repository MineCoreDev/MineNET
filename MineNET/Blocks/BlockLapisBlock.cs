using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockLapisBlock : BlockSolid
    {
        public BlockLapisBlock() : base(BlockFactory.LAPIS_BLOCK)
        {

        }

        public override string Name
        {
            get
            {
                return "LapisBlock";
            }
        }
    }
}
