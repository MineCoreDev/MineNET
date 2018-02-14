using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockBirchStairs : BlockStairsBase
    {
        public BlockBirchStairs(): base(BlockFactory.BIRCH_STAIRS)
        {

        }

        public override string Name
        {
            get
            {
                return "BirchStairs";
            }
        }
    }
}
