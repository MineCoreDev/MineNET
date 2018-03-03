using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockIronBars : Block
    {
        public BlockIronBars() : base(BlockFactory.IRON_BARS)
        {

        }

        public override string Name
        {
            get
            {
                return "IronBars";
            }
        }
    }
}
