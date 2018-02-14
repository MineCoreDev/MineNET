using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockTripwire : Block
    {
        public BlockTripwire() : base(BlockFactory.TRIPWIRE)
        {

        }

        public override string Name
        {
            get
            {
                return "Tripwire";
            }
        }
    }
}
