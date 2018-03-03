using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockBlueGlazedTerracotta : BlockSolid
    {
        public BlockBlueGlazedTerracotta() : base(BlockFactory.BLUE_GLAZED_TERRACOTTA)
        {

        }

        public override string Name
        {
            get
            {
                return "BlueGlazedTerracotta";
            }
        }
    }
}
