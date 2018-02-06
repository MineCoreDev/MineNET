using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockPurpurBlock : Block
    {
        public BlockPurpurBlock() : base(BlockFactory.PURPUR_BLOCK)
        {

        }

        public override string Name
        {
            get
            {
                return "PurpurBlock";
            }
        }
    }
}
