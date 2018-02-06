using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockDaylightDetectorInverted : Block
    {
        public BlockDaylightDetectorInverted() : base(BlockFactory.DAYLIGHT_DETECTOR_INVERTED)
        {

        }

        public override string Name
        {
            get
            {
                return "DaylightDetectorInverted";
            }
        }
    }
}
