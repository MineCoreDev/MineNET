using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockRedstoneOre : BlockSolid
    {
        public BlockRedstoneOre() : base(BlockFactory.REDSTONE_ORE)
        {

        }

        public override string Name
        {
            get
            {
                return "RedstoneOre";
            }
        }
    }
}
