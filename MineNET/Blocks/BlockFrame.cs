using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockFrame : BlockTransparent
    {
        public BlockFrame() : base(BlockFactory.FRAME)
        {

        }

        public override string Name
        {
            get
            {
                return "Frame";
            }
        }
    }
}
