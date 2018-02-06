using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockLightGlazedTerracotta : BlockSolid
    {
        public BlockLightGlazedTerracotta() : base(BlockFactory.LIGHT_GLAZED_TERRACOTTA)
        {

        }

        public override string Name
        {
            get
            {
                return "LightGlazedTerracotta";
            }
        }
    }
}
