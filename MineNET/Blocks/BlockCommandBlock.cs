using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockCommandBlock : BlockSolid
    {
        public BlockCommandBlock() : base(BlockFactory.COMMAND_BLOCK)
        {

        }

        public override string Name
        {
            get
            {
                return "CommandBlock";
            }
        }
    }
}
