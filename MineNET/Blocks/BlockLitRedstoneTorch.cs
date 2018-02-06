using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockLitRedstoneTorch : Block
    {
        public BlockLitRedstoneTorch() : base(BlockFactory.LIT_REDSTONE_TORCH)
        {

        } 

        public override string Name
        {
            get
            {
                return "LitRedstoneTorch";
            }
        }
    }
}
