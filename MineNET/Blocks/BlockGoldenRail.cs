using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockGoldenRail : BlockRailBase
    {
        public BlockGoldenRail() : base(BlockFactory.GOLDEN_RAIL)
        {

        }

        public override string Name
        {
            get
            {
                return "GoldenRail";
            }
        }
    }
}
