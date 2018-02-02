using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockDetectorRail : BlockRailBase
    {
        public BlockDetectorRail() : base(BlockFactory.DETECTOR_RAIL)
        {

        }

        public override string Name
        {
            get
            {
                return "DetectorRail";
            }
        }
    }
}
