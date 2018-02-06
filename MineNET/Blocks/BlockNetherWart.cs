using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockNetherWart : Block
    {
        public BlockNetherWart() : base(BlockFactory.NETHER_WART)
        {

        }

        public override string Name
        {
            get
            {
                return "NetherWart";
            }
        }
    }
}
