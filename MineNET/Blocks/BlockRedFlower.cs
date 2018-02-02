using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockRedFlower : Block
    {
        public BlockRedFlower() : base(BlockFactory.RED_FLOWER)
        {

        }

        public override string Name
        {
            get
            {
                return "RedFlower";
            }
        }
    }
}
