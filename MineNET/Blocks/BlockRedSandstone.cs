using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockRedSandstone : BlockSolid
    {
        public BlockRedSandstone() : base(BlockFactory.RED_SANDSTONE)
        {

        }

        public override string Name
        {
            get
            {
                return "RedSandstone";
            }
        }
    }
}
