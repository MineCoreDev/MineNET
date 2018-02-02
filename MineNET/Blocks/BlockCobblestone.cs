using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockCobblestone : BlockSolid
    {
        public BlockCobblestone() : base(BlockFactory.COBBLESTONE)
        {

        }

        public override string Name
        {
            get
            {
                return "Cobblestone";
            }
        }
    }
}
