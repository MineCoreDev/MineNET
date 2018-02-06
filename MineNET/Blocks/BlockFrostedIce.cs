using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockFrostedIce : BlockSolid
    {
        public BlockFrostedIce() : base(BlockFactory.FROSTED_ICE)
        {

        }

        public override string Name
        {
            get
            {
                return "FrostedIce";
            }
        }
    }
}
