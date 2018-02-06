using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockEndGateway : BlockTransparent
    {
        public BlockEndGateway() : base(BlockFactory.END_GATEWAY)
        {

        }

        public override string Name
        {
            get
            {
                return "EndGateway";
            }
        }
    }
}
