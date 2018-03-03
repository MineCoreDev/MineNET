using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockPodzol : BlockSolid
    {
        public BlockPodzol() : base(BlockFactory.PODZOL)
        {

        }

        public override string Name
        {
            get
            {
                return "Podzol";
            }
        }
    }
}
