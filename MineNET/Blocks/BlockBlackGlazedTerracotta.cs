using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockBlackGlazedTerracotta : BlockSolid
    {
        public BlockBlackGlazedTerracotta() : base(BlockFactory.BLACK_GLAZED_TERRACOTTA)
        {

        }

        public override string Name
        {
            get
            {
                return "BlackGlazedTerracotta";
            }
        }
    }
}
