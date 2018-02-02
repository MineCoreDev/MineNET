using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockGoldOre : BlockSolid
    {
        public BlockGoldOre() : base(BlockFactory.GOLD_ORE)
        {

        }

        public override string Name
        {
            get
            {
                return "GoldOre";
            }
        }
    }
}
