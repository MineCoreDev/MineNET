using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockActivatorRail : BlockRailBase
    {
        public BlockActivatorRail() : base(BlockFactory.ACTIVATOR_RAIL)
        {

        }

        public override string Name
        {
            get
            {
                return "ActivatorRail";
            }
        }
    }
}
