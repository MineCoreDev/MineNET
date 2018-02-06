using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockLitFurnace : BlockSolid
    {
        public BlockLitFurnace() : base(BlockFactory.LIT_FURNACE)
        {

        }

        public override string Name
        {
            get
            {
                return "LitFurnace";
            }
        }
    }
}
