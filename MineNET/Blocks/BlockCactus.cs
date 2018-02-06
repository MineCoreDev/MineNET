using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockCactus : Block
    {
        public BlockCactus() : base(BlockFactory.CACTUS)
        {

        }

        public override string Name
        {
            get
            {
                return "Cactus";
            }
        }
    }
}
