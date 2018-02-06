using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockBrownMushroomBlock : BlockSolid
    {
        public BlockBrownMushroomBlock() : base(BlockFactory.BROWN_MUSHROOM_BLOCK)
        {

        }

        public override string Name
        {
            get
            {
                return "BrownMushroomBlock";
            }
        }
    }
}
