using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockTrappedChest : Block
    {
        public BlockTrappedChest() : base(BlockFactory.TRAPPED_CHEST)
        {

        }

        public override string Name
        {
            get
            {
                return "TrappedChest";
            }
        }
    }
}
