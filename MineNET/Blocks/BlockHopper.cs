using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Blocks
{
    public class BlockHopper : BlockSolid
    {
        public BlockHopper() : base(BlockFactory.HOPPER)
        {

        }

        public override string Name
        {
            get
            {
                return "Hopper";
            }
        }
    }
}
