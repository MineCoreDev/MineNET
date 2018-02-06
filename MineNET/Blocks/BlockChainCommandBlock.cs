using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockChainCommandBlock : BlockSolid
    {
        public BlockChainCommandBlock() : base(BlockFactory.CHAIN_COMMAND_BLOCK)
        {

        }

        public override string Name
        {
            get
            {
                return "ChainCommandBlock";
            }
        }
    }
}
