using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockReeds : BlockTransparent
    {
        public BlockReeds() : base(BlockFactory.REEDS)
        {

        }

        public override string Name
        {
            get
            {
                return "Reeds";
            }
        }
    }
}
