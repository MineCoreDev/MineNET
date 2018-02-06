using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockStainedGlassPane : Block
    {
        public BlockStainedGlassPane() : base(BlockFactory.STAINED_GLASS_PANE)
        {

        }

        public override string Name
        {
            get
            {
                return "StainedGlassPane";
            }
        }
    }
}
