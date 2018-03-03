using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockTnt : BlockSolid
    {
        public BlockTnt() : base(BlockFactory.TNT)
        {

        }

        public override string Name
        {
            get
            {
                return "Tnt";
            }
        }
    }
}
