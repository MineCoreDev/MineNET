using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockWheat : Block
    {
        public BlockWheat() : base(BlockFactory.WHEAT)
        {

        }

        public override string Name
        {
            get
            {
                return "Wheat";
            }
        }
    }
}
