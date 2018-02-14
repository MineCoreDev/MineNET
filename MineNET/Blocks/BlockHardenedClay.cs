using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockHardenedClay : BlockSolid
    {
        public BlockHardenedClay() : base(BlockFactory.HARDENED_CLAY)
        {

        }

        public override string Name
        {
            get
            {
                return "HardenedClay";
            }
        }
    }
}
