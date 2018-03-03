using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockRepeatingCommandBlock : BlockSolid
    {
        public BlockRepeatingCommandBlock() : base(BlockFactory.REPEATING_COMMAND_BLOCK)
        {

        }

        public override string Name
        {
            get
            {
                return "RepeatingCommandBlock";
            }
        }
    }
}
