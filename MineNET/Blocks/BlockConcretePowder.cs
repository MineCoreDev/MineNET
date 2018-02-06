using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockConcretePowder : BlockSolid
    {
        public BlockConcretePowder() : base(BlockFactory.CONCRETE_POWDER)
        {

        }

        public override string Name
        {
            get
            {
                return "ConcretePowder";
            }
        }
    }
}
