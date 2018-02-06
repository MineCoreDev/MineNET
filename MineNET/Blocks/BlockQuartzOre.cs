using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockQuartzOre : BlockSolid
    {
        public BlockQuartzOre() : base(BlockFactory.QUARTZ_ORE)
        {

        }

        public override string Name
        {
            get
            {
                return "QuartzOre";
            }
        }
    }
}
