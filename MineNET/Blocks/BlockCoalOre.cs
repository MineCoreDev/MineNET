using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockCoalOre : BlockSolid
    {
        public BlockCoalOre() : base(BlockFactory.COAL_ORE)
        {

        }

        public override string Name
        {
            get
            {
                return "CoalOre";
            }
        }
    }
}
