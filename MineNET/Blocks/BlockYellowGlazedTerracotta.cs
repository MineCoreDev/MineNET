using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockYellowGlazedTerracotta : BlockSolid
    {
        public BlockYellowGlazedTerracotta() : base(BlockFactory.YELLOW_GLAZED_TERRACOTTA)
        {

        }

        public override string Name
        {
            get
            {
                return "YellowGlazedTerracotta";
            }
        }
    }
}
