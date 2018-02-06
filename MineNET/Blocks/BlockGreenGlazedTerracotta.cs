using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockGreenGlazedTerracotta : BlockSolid
    {
        public BlockGreenGlazedTerracotta() : base(BlockFactory.GREEN_GLAZED_TERRACOTTA)
        {

        }

        public override string Name
        {
            get
            {
                return "GreenGlazedTerracotta";
            }
        }
    }
}
