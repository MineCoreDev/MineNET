using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    class BlockDirt : BlockSolid
    {
        public BlockDirt() : base(BlockFactory.DIRT)
        {

        }

        public override string Name
        {
            get
            {
                return "Dirt";
            }
        }
    }
}
