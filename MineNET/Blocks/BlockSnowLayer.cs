using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockSnowLayer : Block
    {
        public BlockSnowLayer() : base(BlockFactory.SNOW_LAYER)
        {

        }

        public override string Name
        {
            get
            {
                return "SnowLayer";
            }
        }
    }
}
