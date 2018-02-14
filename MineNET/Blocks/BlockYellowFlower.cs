using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockYellowFlower : Block
    {
        public BlockYellowFlower() : base(BlockFactory.YELLOW_FLOWER)
        {

        }

        public override string Name
        {
            get
            {
                return "YellowFlower";
            }
        }
    }
}
