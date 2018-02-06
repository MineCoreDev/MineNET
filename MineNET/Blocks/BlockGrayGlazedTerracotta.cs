using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockGrayGlazedTerracotta : BlockSolid
    {
        public BlockGrayGlazedTerracotta() : base(BlockFactory.GRAY_GLAZED_TERRACOTTA)
        {

        }

        public override string Name
        {
            get
            {
                return "GrayGlazedTerracotta";
            }
        }
    }
}
