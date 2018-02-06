using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockGlassPane : Block
    {
        public BlockGlassPane() : base(BlockFactory.GLASS_PANE)
        {

        }

        public override string Name
        {
            get
            {
                return "GlassPane";
            }
        }
    }
}
