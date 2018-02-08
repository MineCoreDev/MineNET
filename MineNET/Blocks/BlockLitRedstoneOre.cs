using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockLitRedstoneOre : Block
    {
        public BlockLitRedstoneOre() : base(BlockFactory.LIT_REDSTONE_ORE)
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
