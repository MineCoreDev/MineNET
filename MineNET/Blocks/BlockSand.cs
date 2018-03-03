using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockSand : BlockSolid
    {
        public BlockSand() : base(BlockFactory.SAND)
        {

        }

        public override string Name
        {
            get
            {
                return "Sand";
            }
        }
    }
}
