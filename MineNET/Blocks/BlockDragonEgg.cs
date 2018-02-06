using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockDragonEgg : Block
    {
        public BlockDragonEgg() : base(BlockFactory.DRAGON_EGG)
        {

        }

        public override string Name
        {
            get
            {
                return "DragonEgg";
            }
        }
    }
}
