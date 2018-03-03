using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockPumpkinStem : BlockTransparent
    {
        public BlockPumpkinStem() : base(BlockFactory.PUMPKIN_STEM)
        {

        }

        public override string Name
        {
            get
            {
                return "PumpkinStem";
            }
        }
    }
}
