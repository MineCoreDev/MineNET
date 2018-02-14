using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockPlanks : Block
    {
        public BlockPlanks() : base(BlockFactory.PLANKS)
        {

        }

        public override string Name
        {
            get
            {
                return "Planks";
            }
        }
    }
}
