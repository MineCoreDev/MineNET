using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockBrownMushroom : Block
    {
        public BlockBrownMushroom() : base(BlockFactory.BROWN_MUSHROOM)
        {

        }

        public override string Name
        {
            get
            {
                return "BrownMushroom";
            }
        }
    }
}
