using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockPurpurStairs : BlockStairsBase
    {
        public BlockPurpurStairs() : base(BlockFactory.PURPUR_STAIRS)
        {

        }

        public override string Name
        {
            get
            {
                return "PurpurStairs";
            }
        }
    }
}
