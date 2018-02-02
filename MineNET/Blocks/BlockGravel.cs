using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockGravel : BlockSolid
    {
        public BlockGravel() : base(BlockFactory.GRAVEL)
        {

        }

        public override string Name
        {
            get
            {
                return "Gravel";
            }
        }
    }
}
