using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockMycelium : BlockSolid 
    {
        public BlockMycelium() : base(BlockFactory.MYCELIUM)
        {

        }

        public override string Name
        {
            get
            {
                return "Mycelium";
            }
        }
    }
}
