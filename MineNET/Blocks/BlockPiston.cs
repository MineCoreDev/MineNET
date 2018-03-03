using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockPiston : BlockSolid
    {
        public BlockPiston() : base(BlockFactory.PISTON)
        {

        }

        public override string Name
        {
            get
            {
                return "Piston";
            }
        }
    }
}
