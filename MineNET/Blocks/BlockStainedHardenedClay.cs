using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockStainedHardenedClay : BlockSolid
    {
        public BlockStainedHardenedClay() : base(BlockFactory.STAINED_HARDENED_CLAY)
        {

        }

        public override string Name
        {
            get
            {
                return "StainedHardenedClay";
            }
        }
    }
}
