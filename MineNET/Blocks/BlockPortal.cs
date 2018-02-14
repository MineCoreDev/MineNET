using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockPortal : BlockTransparent
    {
        public BlockPortal() : base(BlockFactory.PORTAL)
        {

        }

        public override string Name
        {
            get
            {
                return "Portal";
            }
        }
    }
}
