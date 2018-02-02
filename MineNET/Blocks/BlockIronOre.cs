using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockIronOre : BlockSolid
    {
        public BlockIronOre() : base(BlockFactory.IRON_ORE)
        {

        }

        public override string Name
        {
            get
            {
                return "IronOre";
            }
        }
    }
}
