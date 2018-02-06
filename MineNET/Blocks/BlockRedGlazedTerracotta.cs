using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockRedGlazedTerracotta : BlockSolid
    {
        public BlockRedGlazedTerracotta() : base(BlockFactory.RED_GLAZED_TERRACOTTA)
        {

        }

        public override string Name
        {
            get
            {
                return "RedGlazedTerracotta";
            }
        }
    }
}
