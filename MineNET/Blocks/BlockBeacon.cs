using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockBeacon : BlockSolid
    {
        public BlockBeacon() : base(BlockFactory.BEACON)
        {

        }

        public override string Name
        {
            get
            {
                return "Beacon";
            }
        }
    }
}
