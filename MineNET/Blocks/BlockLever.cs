using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockLever : Block
    {
        public BlockLever() : base(BlockFactory.LEVER)
        {

        }

        public override string Name
        {
            get
            {
                return "Lever";
            }
        }
    }
}
