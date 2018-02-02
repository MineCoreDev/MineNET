using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockWater : Block
    {
        public BlockWater() : base(BlockFactory.WATER)
        {

        }

        public override string Name
        {
            get
            {
                return "Water";
            }
        }
    }
}
