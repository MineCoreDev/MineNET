using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockWaterlily : Block
    {
        public BlockWaterlily() : base(BlockFactory.WATERLILY)
        {

        }

        public override string Name
        {
            get
            {
                return "Waterlily";
            }
        }
    }
}
