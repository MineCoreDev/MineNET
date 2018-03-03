using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockCocoa : Block
    {
        public BlockCocoa() : base(BlockFactory.COCOA)
        {

        }

        public override string Name
        {
            get
            {
                return "Cocoa";
            }
        }
    }
}
