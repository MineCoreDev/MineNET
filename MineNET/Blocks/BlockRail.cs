using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockRail : BlockRailBase
    {
        public BlockRail() : base(BlockFactory.RAIL)
        {

        }

        public override string Name
        {
            get
            {
                return "Rail";
            }
        }
    }
}
