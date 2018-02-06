using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockWallSign : Block
    {
        public BlockWallSign() : base(BlockFactory.WALL_SIGN)
        {

        }

        public override string Name
        {
            get
            {
                return "WallSign";
            }
        }
    }
}
