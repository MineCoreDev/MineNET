using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockPurpleGlazedTerracotta : BlockSolid
    {
        public BlockPurpleGlazedTerracotta() : base(BlockFactory.PURPLE_GLAZED_TERRACOTTA)
        {

        }

        public override string Name
        {
            get
            {
                return "PurpleGlazedTerracotta";
            }
        }
    }
}
