using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockGlass : BlockSolid
    {
        public BlockGlass() : base(BlockFactory.GLASS)
        {

        }

        public override string Name
        {
            get
            {
                return "Glass";
            }
        }
    }
}
