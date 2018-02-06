using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockSlime : Block
    {
        public BlockSlime() : base(BlockFactory.SLIME)
        {

        }

        public override string Name
        {
            get
            {
                return "Slime";
            }
        }
    }
}
