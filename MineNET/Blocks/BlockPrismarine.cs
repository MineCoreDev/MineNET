using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockPrismarine : BlockSolid
    {
        public BlockPrismarine() : base(BlockFactory.PRISMARINE)
        {

        }

        public override string Name
        {
            get
            {
                return "Prismarine";
            }
        }
    }
}
