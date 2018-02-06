using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockMagma : BlockSolid
    {
        public BlockMagma() : base(BlockFactory.MAGMA)
        {

        }

        public override string Name
        {
            get
            {
                return "Magma";
            }
        }
    }
}
