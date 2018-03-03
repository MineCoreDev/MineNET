using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockWhiteGlazedTerracotta : BlockSolid
    {
        public BlockWhiteGlazedTerracotta() : base(BlockFactory.WHITE_GLAZED_TERRACOTTA)
        {

        }

        public override string Name
        {
            get
            {
                return "WhiteGlazedTerracotta";
            }
        }
    }
}
