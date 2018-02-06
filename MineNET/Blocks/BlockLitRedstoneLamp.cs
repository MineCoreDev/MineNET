using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockLitRedstoneLamp : BlockSolid
    {
        public BlockLitRedstoneLamp() : base(BlockFactory.LIT_REDSTONE_LAMP)
        {

        }

        public override string Name
        {
            get
            {
                return "LitRedstoneLamp";
            }
        }
    }
}
