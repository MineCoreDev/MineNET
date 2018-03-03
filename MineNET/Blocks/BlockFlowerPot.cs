using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockFlowerPot : Block
    {
        public BlockFlowerPot() : base(BlockFactory.FLOWER_POT)
        {

        }

        public override string Name
        {
            get
            {
                return "FlowerPot";
            }
        }
    }
}
