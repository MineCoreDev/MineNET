using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockPinkGlazedTerracotta : BlockSolid
    {
        public BlockPinkGlazedTerracotta() : base(BlockFactory.PINK_GLAZED_TERRACOTTA)
        {

        }

        public override string Name
        {
            get
            {
                return "PinkGlazedTerracotta";
            }
        }
    }
}
