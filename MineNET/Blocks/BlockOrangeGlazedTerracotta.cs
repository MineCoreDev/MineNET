using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockOrangeGlazedTerracotta : BlockSolid
    {
        public BlockOrangeGlazedTerracotta() : base(BlockFactory.ORANGE_GLAZED_TERRACOTTA)
        {

        }

        public override string Name
        {
            get
            {
                return "OrangeGlazedTerracotta";
            }
        }
    }
}
