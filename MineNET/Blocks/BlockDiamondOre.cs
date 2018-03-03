using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockDiamondOre : BlockSolid
    {
        public BlockDiamondOre() : base(BlockFactory.DIAMOND_ORE)
        {

        }

        public override string Name
        {
            get
            {
                return "DiamondOre";
            }
        }
    }
}
