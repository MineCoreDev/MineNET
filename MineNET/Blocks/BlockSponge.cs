using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockSponge : BlockSolid
    {
        public BlockSponge() : base(BlockFactory.SPONGE)
        {

        }

        public override string Name
        {
            get
            {
                return "Sponge";
            }
        }
    }
}
