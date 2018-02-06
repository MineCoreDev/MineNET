using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockStandingBanner : BlockTransparent
    {
        public BlockStandingBanner() : base(BlockFactory.STANDING_BANNER)
        {

        }

        public override string Name
        {
            get
            {
                return "StandingBanner";
            }
        }
    }
}
