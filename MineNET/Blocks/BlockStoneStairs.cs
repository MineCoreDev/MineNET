using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockStoneStairs : BlockStairsBase
    {
        public BlockStoneStairs() : base(BlockFactory.STONE_STAIRS)
        {

        }

        public override string Name
        {
            get
            {
                return "StoneStairs";
            }
        }
    }
}
