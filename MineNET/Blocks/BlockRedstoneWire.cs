using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockRedstoneWire : Block
    {
        public BlockRedstoneWire() : base(BlockFactory.REDSTONE_WIRE)
        {

        }

        public override string Name
        {
            get
            {
                return "RedstoneWire";
            }
        }
    }
}
