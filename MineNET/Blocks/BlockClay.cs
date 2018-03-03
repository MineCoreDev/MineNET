using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockClay : BlockSolid
    {
        public BlockClay() : base(BlockFactory.CLAY)
        {

        }

        public override string Name
        {
            get
            {
                return "Clay";
            }
        }
    }
}
