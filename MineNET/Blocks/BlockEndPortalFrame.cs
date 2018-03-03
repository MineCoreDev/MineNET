using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockEndPortalFrame : Block
    {
        public BlockEndPortalFrame() : base(BlockFactory.END_PORTAL_FRAME)
        {

        }

        public override string Name
        {
            get
            {
                return "EndPortalFrame";
            }
        }
    }
}
