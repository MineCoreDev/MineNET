using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockMagentaGlazedTerracotta : BlockSolid
    {
        public BlockMagentaGlazedTerracotta() : base(BlockFactory.MAGENTA_GLAZED_TERRACOTTA)
        {

        }

        public override string Name
        {
            get
            {
                return "MagentaGlazedTerracotta";
            }
        }
    }
}
