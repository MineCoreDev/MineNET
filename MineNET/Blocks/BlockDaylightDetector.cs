using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockDaylightDetector : Block
    {
        public BlockDaylightDetector() : base(BlockFactory.DAYLIGHT_DETECTOR)
        {

        }

        public override string Name
        {
            get
            {
                return "DaylightDetector";
            }
        }
    }
}
