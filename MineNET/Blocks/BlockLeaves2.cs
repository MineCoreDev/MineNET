using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockLeaves2 : BlockTransparent
    {
        public BlockLeaves2() : base(BlockFactory.LEAVES2)
        {

        }

        public override string Name
        {
            get
            {
                return "Leaves2";
            }
        }
    }
}
