using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockGlazedTerracottaBrown : BlockSolid
    {
        public BlockGlazedTerracottaBrown() : base(BlockFactory.BROWN_GLAZED_TERRACOTTA)
        {

        }

        public override string Name
        {
            get
            {
                return "BrownGlazedTerracotta";
            }
        }
    }
}
