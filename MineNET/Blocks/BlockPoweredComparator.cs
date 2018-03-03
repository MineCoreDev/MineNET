using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockPoweredComparator : Block
    {
        public BlockPoweredComparator() : base(BlockFactory.POWERED_COMPARATOR)
        {

        }

        public override string Name
        {
            get
            {
                return "PoweredComparator";
            }
        }
    }
}
