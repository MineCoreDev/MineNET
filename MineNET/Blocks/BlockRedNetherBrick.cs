using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockRedNetherBrick : BlockSolid
    {
        public BlockRedNetherBrick() : base(BlockFactory.RED_NETHER_BRICK)
        {

        }

        public override string Name
        {
            get
            {
                return "RedNetherBrick";
            }
        }
    }
}
