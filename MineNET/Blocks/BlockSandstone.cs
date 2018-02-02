using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockSandstone : BlockSolid
    {
        public BlockSandstone() : base(BlockFactory.SANDSTONE)
        {

        }

        public override string Name
        {
            get
            {
                return "Sandstone";
            }
        }
    }
}
