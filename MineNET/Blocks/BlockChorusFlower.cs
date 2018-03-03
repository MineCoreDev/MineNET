using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockChorusFlower : Block
    {
        public BlockChorusFlower() : base(BlockFactory.CHORUS_FLOWER)
        {

        }

        public override string Name
        {
            get
            {
                return "ChorusFlower";
            }
        }
    }
}
