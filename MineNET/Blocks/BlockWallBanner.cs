using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockWallBanner : BlockTransparent
    {
        public BlockWallBanner() : base(BlockFactory.WALL_BANNER)
        {

        }

        public override string Name
        {
            get
            {
                return "WallBanner";
            }
        }
    }
}
