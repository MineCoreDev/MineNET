using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockLimeGlazedTerracotta : BlockSolid
    {
        public BlockLimeGlazedTerracotta() : base(BlockFactory.LIME_GLAZED_TERRACOTTA)
        {

        }

        public override string Name
        {
            get
            {
                return "LimeGlazedTerracotta";
            }
        }
    }
}
