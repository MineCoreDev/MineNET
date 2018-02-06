using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockTripwireHook : Block
    {
        public BlockTripwireHook() : base(BlockFactory.TRIPWIRE_HOOK)
        {

        }

        public override string Name
        {
            get
            {
                return "TripwireHook";
            }
        }
    }
}
