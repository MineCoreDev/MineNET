using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockNetherBrick : BlockSolid
    {
        public BlockNetherBrick() : base(BlockFactory.NETHER_BRICK)
        {

        }

        public override string Name
        {
            get
            {
                return "NetherBrick";
            }
        }
    }
}
