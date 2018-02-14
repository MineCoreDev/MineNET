using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockMelonBlock : BlockSolid
    {
        public BlockMelonBlock() : base(BlockFactory.MELON_BLOCK)
        {

        }

        public override string Name
        {
            get
            {
                return "MelonBlock";
            }
        }
    }
}
