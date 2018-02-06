using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockTorch : BlockSolid
    {
        public BlockTorch() : base(BlockFactory.TORCH)
        {

        }

        public override string Name
        {
            get
            {
                return "Torch";
            }
        }
    }
}
