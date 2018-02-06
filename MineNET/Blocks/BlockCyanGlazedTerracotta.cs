using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockCyanGlazedTerracotta : BlockSolid
    {
        public BlockCyanGlazedTerracotta() : base(BlockFactory.CYAN_GLAZED_TERRACOTTA)
        {

        }

        public override string Name
        {
            get
            {
                return "CyanGlazedTerracotta";
            }
        }
    }
}
