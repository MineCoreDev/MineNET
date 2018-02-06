using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockEmeraldOre : BlockSolid
    {
        public BlockEmeraldOre() : base(BlockFactory.EMERALD_ORE)
        {

        }

        public override string Name
        {
            get
            {
                return "EmeraldOre";
            }
        }
    }
}
