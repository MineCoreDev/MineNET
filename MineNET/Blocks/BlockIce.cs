using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockIce : BlockSolid
    {
        public BlockIce() : base(BlockFactory.ICE)
        {

        }

        public override string Name
        {
            get
            {
                return "Ice";
            }
        }
    }
}
