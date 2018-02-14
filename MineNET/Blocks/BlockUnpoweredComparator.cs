using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockUnpoweredComparator : Block
    {
        public BlockUnpoweredComparator() : base(BlockFactory.UNPOWERED_COMPARATOR)
        {

        }

        public override string Name
        {
            get
            {
                return "UnpoweredComparator";
            }
        }
    }
}
