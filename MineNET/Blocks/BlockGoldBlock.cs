using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockGoldBlock : BlockSolid
    {
        public BlockGoldBlock() : base(BlockFactory.GOLD_BLOCK)
        {

        }

        public override string Name
        {
            get
            {
                return "GoldBlock";
            }
        }
    }
}
