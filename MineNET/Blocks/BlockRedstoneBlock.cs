using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockRedstoneBlock : BlockSolid
    {
        public BlockRedstoneBlock() : base(BlockFactory.REDSTONE_BLOCK)
        {

        }

        public override string Name
        {
            get
            {
                return "RedstoneBlock";
            }
        }
    }
}
