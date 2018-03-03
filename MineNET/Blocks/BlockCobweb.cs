using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockCobweb : BlockSolid
    {
        public BlockCobweb() : base(BlockFactory.COBWEB)
        {

        }

        public override string Name
        {
            get
            {
                return "Cobweb";
            }
        }
    }
}
