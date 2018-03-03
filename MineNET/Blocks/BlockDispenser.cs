using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockDispenser : BlockSolid
    {
        public BlockDispenser() : base(BlockFactory.DISPENSER)
        {

        }

        public override string Name
        {
            get
            {
                return "Dispenser";
            }
        }
    }
}
