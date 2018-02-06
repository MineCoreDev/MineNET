using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockPackedIce : BlockSolid
    {
        public BlockPackedIce() : base(BlockFactory.PACKED_ICE)
        {

        }

        public override string Name
        {
            get
            {
                return "PackedIce";
            }
        }
    }
}
