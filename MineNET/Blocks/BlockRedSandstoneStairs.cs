using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockRedSandstoneStairs : BlockStairsBase
    {
        public BlockRedSandstoneStairs() : base(BlockFactory.RED_SANDSTONE_STAIRS)
        {

        }

        public override string Name
        {
            get
            {
                return "RedSandstoneStairs";
            }
        }
    }
}
