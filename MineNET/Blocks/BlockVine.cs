using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockVine : BlockTransparent
    {
        public BlockVine() : base(BlockFactory.VINE)
        {

        }

        public override string Name
        {
            get
            {
                return "Vine";
            }
        }
    }
}
