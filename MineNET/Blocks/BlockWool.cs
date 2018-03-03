using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockWool : BlockSolid
    {
        public BlockWool() : base(BlockFactory.WOOL)
        {

        }

        public override string Name
        {
            get
            {
                return "Wool";
            }
        }
    }
}
