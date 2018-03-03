using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockRedstoneTorch : Block
    {
        public BlockRedstoneTorch() : base(BlockFactory.REDSTONE_TORCH)
        {

        }

        public override string Name
        {
            get
            {
                return "RedstoneTorch";
            }
        }
    }
}
