using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockSilverGlazedTerracotta : BlockSolid
    {
        public BlockSilverGlazedTerracotta() : base(BlockFactory.SILVER_GLAZED_TERRACOTTA)
        {

        }

        public override string Name
        {
            get
            {
                return "SilverGlazedTerracotta";
            }
        }
    }
}
