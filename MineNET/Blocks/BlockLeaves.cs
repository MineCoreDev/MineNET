using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockLeaves : BlockSolid
    {
        public BlockLeaves() : base(BlockFactory.LEAVES)
        {

        } 

        public override string Name
        {
            get
            {
                return "Leaves";
            }
        }
    }
}
