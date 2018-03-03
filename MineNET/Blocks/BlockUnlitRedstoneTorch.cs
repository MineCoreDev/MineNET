using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockUnlitRedstoneTorch : Block
    {
        public BlockUnlitRedstoneTorch() : base(BlockFactory.UNLIT_REDSTONE_TORCH)
        {

        }

        public override string Name
        {
            get
            {
                return "UnlitRedstoneTorch";
            }
        }
    }
}
