using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockCobblestoneWall : Block
    {
        public BlockCobblestoneWall() : base(BlockFactory.COBBLESTONE_WALL)
        {

        }

        public override string Name
        {
            get
            {
                return "CobblestoneWall";
            }
        }
    }
}
