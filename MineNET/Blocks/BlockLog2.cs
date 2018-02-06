using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockLog2 : BlockSolid
    {
        public BlockLog2() : base(BlockFactory.LOG2)
        {

        }

        public override string Name
        {
            get
            {
                return "Log2";
            }
        }
    }
}
